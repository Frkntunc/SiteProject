using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Site.WebUI.HttpClients;
using Site.WebUI.Models.Apartments;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Site.WebUI.Controllers
{
    public class ApartmentController : Controller
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

            if (jwt == null || jwtRole != "Admin")
            {
                TempData["ErrorMessage"] = "İşlem yapmaya yetkiniz yok!";
                return RedirectToAction("Index", "Home");
            }

            var apartmentJson = await MyHttpClient.HttpGet("GET", "Apartments",jwt);
            var apartmentList = JsonConvert.DeserializeObject<List<ApartmentListModel>>(apartmentJson);

            return View(apartmentList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddApartmentModel addApartmentModel)
        {
            var jsonData = JsonConvert.SerializeObject(addApartmentModel);

            var jwt = Request.Cookies["jwt"];

            if (jwt==null)
            {
                TempData["ErrorMessage"] = "İşlem yapmaya yetkiniz yok!";
                return View();
            }

            var result = await MyHttpClient.HttpCommand("POST", jsonData, "Apartments",jwt);

            TempData["Message"] = result;

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var jwt = Request.Cookies["jwt"];

            var apartmentJson = await MyHttpClient.HttpGet("GET", $"Apartments/{id}",jwt);
            var apartment = JsonConvert.DeserializeObject<UpdateApartmentModel>(apartmentJson);

            return View(apartment);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateApartmentModel updateApartmentModel)
        {
            var jsonData = JsonConvert.SerializeObject(updateApartmentModel);
            var jwt = Request.Cookies["jwt"];
            var result = await MyHttpClient.HttpCommand("PUT", jsonData, "Apartments",jwt);

            TempData["Message"] = result;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var jwt = Request.Cookies["jwt"];
            var jsonData = JsonConvert.SerializeObject(id);
            var result = await MyHttpClient.HttpCommand("DELETE", jsonData, $"Apartments/{id}",jwt);

            TempData["Message"] = result;

            return RedirectToAction("Index");
        }

    }
}
