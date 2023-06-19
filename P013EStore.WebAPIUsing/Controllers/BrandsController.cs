using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Controllers
{
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;

        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiAdres = "https://localhost:44335/api/Brands/";
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null)
            {
                BadRequest();
            }
            var model = await _httpClient.GetFromJsonAsync<Brand>(_apiAdres + id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
