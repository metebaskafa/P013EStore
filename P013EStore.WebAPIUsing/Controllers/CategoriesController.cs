﻿using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly HttpClient _httpClient;

		public CategoriesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		private readonly string _apiAdres = "https://localhost:44335/api/";
		public async Task<IActionResult> IndexAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id);
			if (model == null)
			{
				return NotFound();
			}
			 
			return View(model);
		}
	}
}
