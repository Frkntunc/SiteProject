using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site.WebUI.HttpClients;
using Site.WebUI.Models.SignIn;
using System;
using System.Threading.Tasks;

namespace Site.WebUI.Controllers
{
    public class SignInController : Controller
    {
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var jsonData = JsonConvert.SerializeObject(signInModel);
            string jwt = "";
            var result = await MyHttpClient.HttpCommand("POST", jsonData, "Auth",jwt);

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("jwt", result, options);

            return RedirectToAction("Index","Home");
        }
    }
}
