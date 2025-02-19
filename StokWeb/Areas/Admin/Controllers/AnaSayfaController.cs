using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StokWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,User")]
    public class AnaSayfaController : Controller
    {
        private readonly HttpClient _client;

        public AnaSayfaController(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
