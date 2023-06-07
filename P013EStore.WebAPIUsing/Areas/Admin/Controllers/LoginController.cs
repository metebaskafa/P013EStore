using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013EStore.Core.Entities;
using P013EStore.WebAPIUsing.Models;
using System.Security.Claims;

namespace P013EStore.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class LoginController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiAdres = "https://localhost:44335/api/AppUsers";
		public LoginController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public IActionResult Index(string ReturnUrl)
		{
			var model = new AdminLoginViewModel();
			model.ReturnUrl = ReturnUrl;
			return View(model);
		}
		[Route("Logout")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			return Redirect("/Admin/Login");
		}
		[HttpPost]
		public async Task<IActionResult> IndexAsync(AdminLoginViewModel adminLoginViewModel)
		{
			try
			{
				var userList = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres);
				var account = userList.FirstOrDefault(x => x.Email == adminLoginViewModel.Email && x.Password == adminLoginViewModel.Password && x.IsActive);
				if (account == null)
				{
					ModelState.AddModelError("", "Giriş Başarısız");

				}
				else
				{
					var kullaniciYetkileri = new List<Claim>
					{
						new Claim(ClaimTypes.Email, account.Email),
						new Claim("Role", account.IsAdmin ? "Admin" : "User"),
						new Claim("UserGuid", account.UserGuid.ToString())
					};
					var kullaniciKimligi = new ClaimsIdentity(kullaniciYetkileri, "Login");
					ClaimsPrincipal principal = new(kullaniciKimligi);
					await HttpContext.SignInAsync(principal); //HttpContext . net içerisinden geliyor ve uygulamanın çalışma anındaki içeriğe ulaşmamızı sağlıyor. SignInAsync metodu da .net içerisinde mevcut login işlemi yapmak istersek buradaki gibi kullanıyoruz
					return Redirect(string.IsNullOrEmpty(adminLoginViewModel.ReturnUrl) ? "/Admin/Main" : adminLoginViewModel.ReturnUrl);
				}
			}
			catch (Exception)
			{

				ModelState.AddModelError("", "Hata Oluştu!");
			}
			
			return View(adminLoginViewModel);
		}
	}
}
