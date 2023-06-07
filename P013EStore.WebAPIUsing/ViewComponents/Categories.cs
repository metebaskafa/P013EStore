using P013EStore.Core.Entities;

using Microsoft.AspNetCore.Mvc;

namespace P013EStore.WebAPIUsing.ViewComponents
{
    public class Categories : ViewComponent
    {
		private readonly HttpClient _httpClient;

		public Categories(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		
		public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:44335/api/Categories"));
        }
    }
}
