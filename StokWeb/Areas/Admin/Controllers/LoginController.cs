using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StokWeb.Models;

namespace StokWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {

        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var responseMessage = await _httpClient.GetFromJsonAsync<List<Entity.User>>("https://localhost:7288/api/User/UserList");
            var IsUser = responseMessage.FirstOrDefault(i => i.UserName == model.UserName && i.Password == model.Password);
            if (responseMessage == null || !responseMessage.Any())
            {
                ModelState.AddModelError("", "Kullanıcılar alınamadı.");
                return View();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Formda hata var.");
                return View();
            }
            
            if (ModelState.IsValid)
            { 
                if (IsUser != null)
                {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, IsUser.UserId.ToString()));

                    userClaims.Add(new Claim(ClaimTypes.Name, IsUser.UserName));
                    userClaims.Add(new Claim(ClaimTypes.Role, IsUser.Rol));


                    //cookie varsa siler
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // otomatik beni hatırla
                    var LoginProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), LoginProperties);

                    return RedirectToAction("Index", "AnaSayfa");
                }
                else
                {
                    TempData["StokError"] = "Hatalı kullanıcı adı veya şifre.";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya Parola Yanlış");
            }
            return View(model);
        }
    }        
}


