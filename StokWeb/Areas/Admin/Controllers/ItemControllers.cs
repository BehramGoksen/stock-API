using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,User")]
    public class ItemController : Controller
    {
        private readonly HttpClient _client;

        public ItemController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchQuery = "")
        {

            var responseMessage = await _client.GetFromJsonAsync<List<Item>>("https://localhost:7288/api/Item/ItemList");
            if (!string.IsNullOrEmpty(searchQuery))
            {
                responseMessage = responseMessage.Where(c => c.ItemName.ToLower().Contains(searchQuery.ToLower())).ToList();
            }


            var totalItems = responseMessage.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);


            var pagedItems = responseMessage.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItems"] = totalItems;
            ViewData["SearchQuery"] = searchQuery;

            return View(pagedItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, string itemName, int Quantity)
        {
            var item = new Item
            {
                ItemId = id,
                ItemName = itemName,
                Quantity = Quantity
            };

            var responseMessage = await _client.PostAsJsonAsync("https://localhost:7288/api/Item/CreateItem", item);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {

            var data = await _client.DeleteAsync("https://localhost:7288/api/Item/DeleteItem/" + id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var response = await _client.GetFromJsonAsync<Item>($"https://localhost:7288/api/Item/FindItem?id={id}");
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, string itemname, int Quantity)
        {
            var item = new Item
            {
                ItemId = id,
                ItemName = itemname,
                Quantity = Quantity
            };
            if (Quantity >= 0)
            {
                var responseMessage = await _client.PutAsJsonAsync($"https://localhost:7288/api/Item/UpdateItem/{id}", item);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["StokError"] = "Miktar negatif olamaz";
                return RedirectToAction("Update");
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseStock(int id)
        {
            var item = await _client.GetFromJsonAsync<Item>($"https://localhost:7288/api/Item/FindItem?id={id}");
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseStock(int id, int amount)
        {
            var responseMessage = await _client.PostAsync($"https://localhost:7288/api/Item/IncreaseStock/{id}?amount={amount}", null);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed to increase stock.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DecreaseStock(int id)
        {
            var item = await _client.GetFromJsonAsync<Item>($"https://localhost:7288/api/Item/FindItem?id={id}");
            if (item== null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseStock(int id, int amount)
        {
            var responseMessage = await _client.PostAsync($"https://localhost:7288/api/Item/DecreaseStock/{id}?amount={amount}", null);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["StokError"] = "Yeterli stok bulunamadı.";
                return RedirectToAction("Index");
            }
        }



    }
}
