using Gallery.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
    Args = args
});

// 1️⃣ Додаємо підтримку локалізації
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization() // локалізація представлень (View)
    .AddDataAnnotationsLocalization(); // локалізація атрибутів валідації

// 2️⃣ Налаштовуємо контекст БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3️⃣ Налаштовуємо доступні мови
var supportedCultures = new[]
{
    new CultureInfo("uk"),  // українська
    new CultureInfo("en")   // англійська
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// 4️⃣ Стандартний middleware-пайплайн
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 5️⃣ Роутинг
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
