using P013EStore.Data;
using P013EStore.Service.Abstract;
using P013EStore.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();//uygulamada session kullanabilmek için 

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // kendi yazdýðýmýz db iþlemlerini yapan servisi .net core da bu þekilde mvc projesine servis olarak tanýtýyoruz ki kullanabilelim
builder.Services.AddTransient<IProductService, ProductService>();// product için yazdýðýmýz özel servisi uygulamaya tanýttýk.AddTransient yöntemi ile servis eklediðimizde sistem uygulamayý çalýþtýrdýðýnda hazýrda ayný nesne varsa o kullanýlýr yoksa yeni bir nesne oluþturup kullanýma sunulur

//builder.Services.AddSingleton<IProductService, ProductService>(); // addsingleton yöntemi ile servis eklediðimizde sistem uygulamayý çalýþtýrdýðýnda bu nesneden 1 tane üretir ve her istekte ayný nesne gönderilir. performans olarak diðerlerinden iyi yöntemdir
//builder.Services.AddScoped<IProductService, ProductService>(); // addsingleton yöntemi ile servis eklediðimizde sistem uygulamayý çalýþtýrdýðýnda bu nesneye gelen hjer istek için ayrý ayrý nesneler üretip bunu kullanýma sunar içeriðin çok dinamik bir þekilde sürekli deðiþtiði projelerde kullanýlabilir. döviz altýn fiyatý gibi anlýk deðiþimlerin olduðu projelerde mesela
builder.Services.AddTransient<ICategoryService, CategoryService>();

//uygulama admin paneli için oturum açma ayarlarý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // oturum açmayan kullanýcýlarýn giriþ için gönderileceði adres
    x.LogoutPath = "/Admin/Logout"; 
    x.AccessDeniedPath = "/AccessDenied";// yetkilendirme ile ekrana eriþim hakký olmyan kullanýcýlarýn gönderileceði sayfa
    x.Cookie.Name = "Administrator"; // oluþacak kukinin ismi
    x.Cookie.MaxAge= TimeSpan.FromDays(1);// oluþacak kukinin yaþam süresi(1gün)
}); // oturum  iþlemleri için 

//uygulama admin paneli için admin yetkilendirme ayarlarý
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // admin paneline giriþ yapma yetkisine sahip olanlarý bu kuralla kontrol edeceðiz
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin dýþýnda yetkilendirme kullanýrssak bu kuralý kullanabiliriz(siteye üye giriþi yapanlarý ön yüzde bir panele eriþtirme gibi)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();// session için 

app.UseAuthentication(); // dikkat! önce useAuthentication satýrý gelmeli sonra useAuthorization
app.UseAuthorization();
app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
