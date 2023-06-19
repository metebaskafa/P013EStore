using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiAdres = "https://localhost:44335/api/News/";
        public async Task<IActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdres);
            return View(model);
        }
    }
}
