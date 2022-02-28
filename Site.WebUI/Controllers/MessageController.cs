using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site.WebUI.HttpClients;
using Site.WebUI.Models.Messages;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Site.WebUI.Controllers
{
    public class MessageController : Controller
    {
        public async Task<IActionResult> Index()
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
            
            var messageJson = await MyHttpClient.HttpGet("GET", "Messages", jwt);
            var messageList = JsonConvert.DeserializeObject<List<MessageListModel>>(messageJson);

            return View(messageList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SendMessageModel sendMessageModel)
        {
            var jsonData = JsonConvert.SerializeObject(sendMessageModel);

            var jwt = Request.Cookies["jwt"];

            var result = await MyHttpClient.HttpCommand("POST", jsonData, "Messages", jwt);

            TempData["Message"] = result;

            return RedirectToAction("Index");
        }
    }
}
