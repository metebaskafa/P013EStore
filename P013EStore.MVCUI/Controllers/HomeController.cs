﻿using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.MVCUI.Models;
using P013EStore.MVCUI.Utils;
using P013EStore.Service.Abstract;
using System.Diagnostics;

namespace P013EStore.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Contact> _serviceContact;
        private readonly IService<News> _serviceNews;
        private readonly IService<Brand> _serviceBrand;

        public HomeController(IService<Slider> serviceSlider, IService<Product> serviceProduct, IService<Contact> serviceContact, IService<News> serviceNews, IService<Brand> serviceBrand)
        {
            _serviceSlider = serviceSlider;
            _serviceProduct = serviceProduct;
            _serviceContact = serviceContact;
            _serviceNews = serviceNews;
            _serviceBrand = serviceBrand;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _serviceSlider.GetAllAsync(),
                Products = await _serviceProduct.GetAllAsync(p => p.IsActive && p.IsHome),
                Brands = await _serviceBrand.GetAllAsync(b => b.IsActive),
                News = await _serviceNews.GetAllAsync(n => n.IsActive && n.IsHome)
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("iletisim")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("iletisim"), HttpPost]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc = await _serviceContact.SaveAsync();
                    if (sonuc>0)
                    {
                        // await MailHelper.SendMailAsync(contact); // gelen mesajı mail gönder.
                        TempData["Message"] = "<div class='alert alert-success'> Mesajınız Gönderildi.Teşekkürler... </div>";
                        return RedirectToAction("ContactUs");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata Oluştu!");
                }

            }
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}