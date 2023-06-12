using P013EStore.Data;
using P013EStore.Service.Abstract;
using P013EStore.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();//uygulamada session kullanabilmek i�in 

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // kendi yazd���m�z db i�lemlerini yapan servisi .net core da bu �ekilde mvc projesine servis olarak tan�t�yoruz ki kullanabilelim
builder.Services.AddTransient<IProductService, ProductService>();// product i�in yazd���m�z �zel servisi uygulamaya tan�tt�k.AddTransient y�ntemi ile servis ekledi�imizde sistem uygulamay� �al��t�rd���nda haz�rda ayn� nesne varsa o kullan�l�r yoksa yeni bir nesne olu�turup kullan�ma sunulur

//builder.Services.AddSingleton<IProductService, ProductService>(); // addsingleton y�ntemi ile servis ekledi�imizde sistem uygulamay� �al��t�rd���nda bu nesneden 1 tane �retir ve her istekte ayn� nesne g�nderilir. performans olarak di�erlerinden iyi y�ntemdir
//builder.Services.AddScoped<IProductService, ProductService>(); // addsingleton y�ntemi ile servis ekledi�imizde sistem uygulamay� �al��t�rd���nda bu nesneye gelen hjer istek i�in ayr� ayr� nesneler �retip bunu kullan�ma sunar i�eri�in �ok dinamik bir �ekilde s�rekli de�i�ti�i projelerde kullan�labilir. d�viz alt�n fiyat� gibi anl�k de�i�imlerin oldu�u projelerde mesela
builder.Services.AddTransient<ICategoryService, CategoryService>();

//uygulama admin paneli i�in oturum a�ma ayarlar�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // oturum a�mayan kullan�c�lar�n giri� i�in g�nderilece�i adres
    x.LogoutPath = "/Admin/Logout"; 
    x.AccessDeniedPath = "/AccessDenied";// yetkilendirme ile ekrana eri�im hakk� olmyan kullan�c�lar�n g�nderilece�i sayfa
    x.Cookie.Name = "Administrator"; // olu�acak kukinin ismi
    x.Cookie.MaxAge= TimeSpan.FromDays(1);// olu�acak kukinin ya�am s�resi(1g�n)
}); // oturum  i�lemleri i�in 

//uygulama admin paneli i�in admin yetkilendirme ayarlar�
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // admin paneline giri� yapma yetkisine sahip olanlar� bu kuralla kontrol edece�iz
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin d���nda yetkilendirme kullan�rssak bu kural� kullanabiliriz(siteye �ye giri�i yapanlar� �n y�zde bir panele eri�tirme gibi)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();// session i�in 

app.UseAuthentication(); // dikkat! �nce useAuthentication sat�r� gelmeli sonra useAuthorization
app.UseAuthorization();
app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
