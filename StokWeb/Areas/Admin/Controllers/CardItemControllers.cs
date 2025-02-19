using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,User")]
    public class CardItemControllers : Controller
    {
        private readonly HttpClient _client;

        public CardItemControllers(HttpClient httpClient)
        {
            _client = httpClient;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchQuery = "")
        {

            var responseMessage = await _client.GetFromJsonAsync<List<CardItem>>("https://localhost:7288/api/CardItem/CardList");

            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                responseMessage = responseMessage.Where(c => c.CardName.ToLower().Contains(searchQuery.ToLower())).ToList();
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

        [HttpPost]
        public IActionResult YourAction(List<int> items)
        {
              
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cardList = await _client.GetFromJsonAsync<List<Card>>("https://localhost:7288/api/Card/CardList");
            var itemList = await _client.GetFromJsonAsync<List<Item>>("https://localhost:7288/api/Item/ItemList");
            
            if (cardList == null || itemList == null)
            {
                ViewBag.ErrorMessage = "Kart veya Bileşen Listesi Yüklenemedi.";
                return View();
            }

            ViewBag.CardList = cardList;
            ViewBag.ItemList = itemList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int cardid, string cardname, List<int> ItemIds, List<string> ItemNames, int quantity, List<int> Quantities)
        {

            if (cardid == 0)
            {
                ViewBag.ErrorMessage = "Kart Seçimi Yapılmadı.";
                return View();
            }
            

            var cardstok = await _client.PostAsync($"https://localhost:7288/api/Card/DecreaseStock/{cardid}?amount={quantity}", null);
            if (cardstok.IsSuccessStatusCode)
            {
                for (int i = 0; i < ItemIds.Count; i++)
                {
                    var amount = Quantities[i] * quantity;
                    var itemstok = await _client.PostAsync($"https://localhost:7288/api/Item/DecreaseStock/{ItemIds[i]}?amount={amount}", null);
                    if (!itemstok.IsSuccessStatusCode)
                    {
                        TempData["StokError"] = $"{ItemNames[i]} için yeterli stok bulunamadı.";
                        return RedirectToAction("Create");
                    }
                }
            }
            else
            {
                TempData["StokError"] = $"{cardname} için yeterli stok bulunamadı.";
                return RedirectToAction("Create");
            }
            for (int i = 0; i < ItemIds.Count; i++)
            {
                var carditemdto = new CardItemDTO
                {
                    CardId = cardid,
                    CardName = cardname,
                    ItemId = ItemIds[i],
                    ItemName = ItemNames[i],
                    itemquantity = Quantities[i],
                    Quantity = quantity,

                };
                var responseMessage = await _client.PostAsJsonAsync("https://localhost:7288/api/CardItem/CreateCard", carditemdto);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return View(carditemdto);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            var response = await _client.DeleteAsync($"https://localhost:7288/api/CardItem/DeleteCard/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return View("Error", response);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var response = await _client.GetFromJsonAsync<CardItem>($"https://localhost:7288/api/CardItem/FindCard?id={id}");
            var cardList = await _client.GetFromJsonAsync<List<Card>>("https://localhost:7288/api/Card/CardList");
            var itemList = await _client.GetFromJsonAsync<List<Item>>("https://localhost:7288/api/Item/ItemList");
            
            ViewBag.CardList = cardList;
            ViewBag.ItemList = itemList;
            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id ,int cardid,string cardname, int ItemIds,string itemname, int quantity, int Quantities)
        {
            var carditem = new CardItem
            {
                CardItemId = id,
                CardId = cardid,
                CardName=cardname,
                ItemId = ItemIds,
                ItemName = itemname,
                itemquantity = Quantities,
                Quantity = quantity,
                    
            };

            var responseMessage = await _client.PutAsJsonAsync($"https://localhost:7288/api/CardItem/UpdateCard/{id}", carditem);

            var amount = Quantities * quantity;
            var itemstok = await _client.PostAsync($"https://localhost:7288/api/Item/DecreaseStock/{ItemIds}?amount={amount}", null);
            
            var cardstok = await _client.PostAsync($"https://localhost:7288/api/Card/DecreaseStock/{cardid}?amount={quantity}", null);

            return View("Index");
        }
    }
}
