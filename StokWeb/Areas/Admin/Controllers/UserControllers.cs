using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly HttpClient _client;

        public UserController(HttpClient httpClient)
        {
            _client = httpClient;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchQuery = "")
        {

            var responseMessage = await _client.GetFromJsonAsync<List<User>>("https://localhost:7288/api/User/UserList");

            if (!string.IsNullOrEmpty(searchQuery))
            {
                responseMessage = responseMessage.Where(c => c.UserName.ToLower().Contains(searchQuery.ToLower())).ToList();
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
        public async Task<IActionResult> Create(int id, string userName, string password, string rol)
        {
            var User = new User
            {
                UserId = id,
                UserName = userName,
                Password = password,
                Rol = rol
            };

            var responseMessage = await _client.PostAsJsonAsync($"https://localhost:7288/api/User/CreateUser", User);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(User);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {

            var data = await _client.DeleteAsync($"https://localhost:7288/api/User/DeleteUser/" + id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var response = await _client.GetFromJsonAsync<User>($"https://localhost:7288/api/User/FindUser?id={id}");
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, string userName, string password, string rol)
        {
            var User = new User
            {
                UserId=id,
                UserName = userName,
                Password = password,
                Rol = rol
            };
            var responseMessage = await _client.PutAsJsonAsync($"https://localhost:7288/api/User/UpdateUser/{id}", User);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(User);
        }
    }
}