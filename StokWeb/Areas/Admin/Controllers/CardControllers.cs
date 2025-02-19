using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace StokWeb.Areas.Admin.Controllers
{

    [Area("Admin") ]
    [Authorize(Roles ="Admin,User")]
    public class CardController : Controller
    {
        private readonly HttpClient _client;

        public CardController(HttpClient httpClient) 
        {
            _client = httpClient;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            
            var responseMessage = await _client.GetFromJsonAsync<List<Card>>("https://localhost:7288/api/Card/CardList");

            if (responseMessage == null)
            {
                return View(new List<Card>());
            }

            
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
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        
        [HttpPost]
        
        public async Task<IActionResult> Create(int id, string cardName, int cardQuantity)
        {
            var card = new Card
            {
                CardId = id,
                CardName = cardName,
                CardQuantity = cardQuantity
            };
            
            var responseMessage = await _client.PostAsJsonAsync($"https://localhost:7288/api/Card/CreateCard", card);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(card);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
           
           var data  =  await _client.DeleteAsync($"https://localhost:7288/api/Card/DeleteCard/" + id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            
            var response = await _client.GetFromJsonAsync<Card>($"https://localhost:7288/api/Card/FindCard?id={id}");
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(int id, string cardName, int cardQuantity)
        {
            var card = new Card
            {
                CardId = id,
                CardName = cardName,
                CardQuantity = cardQuantity
            };
            if(cardQuantity >= 0)
            {
                var responseMessage = await _client.PutAsJsonAsync($"https://localhost:7288/api/Card/UpdateCard/{id}", card);
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

            return View(card); 
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseStock(int id)
        {
            var card = await _client.GetFromJsonAsync<Card>($"https://localhost:7288/api/Card/FindCard?id={id}");
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseStock(int id, int amount)
        {
            var responseMessage = await _client.PostAsync($"https://localhost:7288/api/Card/IncreaseStock/{id}?amount={amount}", null);

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
            var card = await _client.GetFromJsonAsync<Card>($"https://localhost:7288/api/Card/FindCard?id={id}");
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseStock(int id, int amount)
        {
            var responseMessage = await _client.PostAsync($"https://localhost:7288/api/Card/DecreaseStock/{id}?amount={amount}", null);

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