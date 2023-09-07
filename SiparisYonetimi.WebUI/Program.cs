using Microsoft.AspNetCore.Authentication.Cookies;
using SiparisYonetimi.Data;
using SiparisYonetimi.Service.Abstract;
using SiparisYonetimi.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//web api ile baðalntý kurup api kullanarak veratabaný iþlemleri yapabilmek için tanýmlandý
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DatabaseContext>();

//oturum açma iþlemleri için tanýmlandý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Admin/Login";
        x.Cookie.Name = "Administrator"; //cookie adý
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
