﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize(Policy = "AdminPolicy")]
	public class AppUsersController : Controller
    {
        private readonly HttpClient _httpClient;// _httpClient nesnesini kullanarak apilere istek gönderebiliriz.

        private readonly string _apiAdres = "https://localhost:44335/api/AppUsers"; // api adresini web api projesini çalıştırdığımızda adres çubuğundan veya herhangi bir controller a istek atarak request url kısmından veya web api projesinde properties altındaki launchsettings.json

        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient; //_httpClient nesnesinin apiye ulaşması için api projesinin de bu projeyle birlikte çalışıyor olması lazım!!
            // aynı anda 2 projeyi çalıştırabilmek için solution a sağ tıklayıp açılan menüden configure startup projects diyip açılan ekrandan aynı anda başlatmak istediğimiz projeleri seçiyoruz.!
        }

        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres); // _httpClient nesnesi içindeki GetFromJsonAsync metodu kendisine verdiğimiz _apiAdres deki url e get isteği gönderir. ve oradan gelen json formatındaki app user listesini <List<AppUser>> nesnesine dönüştürür.
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AppUser collection)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode) // apiden başarılı bir istek kodu geldiyse (200 ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch 
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser collection)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode) // apiden başarılı bir istek kodu geldiyse (200 ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, AppUser collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {

                return View();
            }
                
            
        }
    }
}
