using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site.WebUI.HttpClients;
using Site.WebUI.Models.Payments;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Site.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewData["Message"] = TempData["Message"];
            }

            var jwt = Request.Cookies["jwt"];

            var token = jwt;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var jwtRole = jwtSecurityToken.Claims.First(claim => claim.Value == "Admin" || claim.Value == "User").Value;

            if (jwt == null)
            {
                TempData["ErrorMessage"] = "İşlem yapmaya yetkiniz yok!";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PayModel payModel)
        {
            var jsonData = JsonConvert.SerializeObject(payModel);

            var jwt = Request.Cookies["jwt"];

            var result = await MyHttpClient.HttpCommand("POST", jsonData, "Payments", jwt);

            TempData["Message"] = result;

            return RedirectToAction("Index","Bill");
        }
    }
}
