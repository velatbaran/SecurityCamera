using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SecurityCamera.Data;
using SecurityCamera.Service.IService;
using SecurityCamera.Service.Service;
using SecurityCamera.WebUI.Utils;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "SecurityCamera.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.IOTimeout = TimeSpan.FromMinutes(10);
}); //session kullan

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Account/SignIn";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "SecurityCamera.Account";
    x.Cookie.MaxAge = TimeSpan.FromDays(7);
    x.Cookie.IsEssential = true; // cookie ayarlarýný kalýcý kýlma
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
}); // yetkilendirme

// Register MailHelper with configuration from appsettings.json
builder.Services.AddSingleton<MailHelper>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var smtpSettings = configuration.GetSection("SmtpSettings");
    return new MailHelper(
        smtpSettings["Server"],
        int.Parse(smtpSettings["Port"]),
        smtpSettings["Username"],
        smtpSettings["Password"],
        bool.Parse(smtpSettings["EnableSsl"])
    );
});

builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        PositionClass = ToastPositions.TopRight,
        TimeOut = 3000,
        ProgressBar = true
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();

app.MapControllerRoute(
  name: "admin",
  pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
