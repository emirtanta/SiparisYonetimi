using Microsoft.AspNetCore.Authentication.Cookies;
using SiparisYonetimi.Data;
using SiparisYonetimi.Service.Abstract;
using SiparisYonetimi.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); //session aktif eder

//web api ile baðlantý kurup api kullanarak veratabaný iþlemleri yapabilmek için tanýmlandý
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DatabaseContext>();

//oturum açma iþlemleri için tanýmlandý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Admin/Login";
        x.LogoutPath = "/Admin/Logout";
        x.AccessDeniedPath = "/AccessDenied";
        x.Cookie.Name = "Administrator"; //cookie adý
        x.Cookie.MaxAge = TimeSpan.FromDays(1); //olusan cookie'nin sam suresi(1gün)
    });

//Authorization: giris yapan kullanicinin yetkilerinin belirlenmesi
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin")); //Admin kullanýcý yetkisi
    options.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User")); //ziyaretçi yetkisi
});

//servisler
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient(typeof(ICategoryService),typeof(CategoryService));
builder.Services.AddTransient(typeof(IBrandService),typeof(BrandService));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); //session ara katmanini aktif ettik

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
