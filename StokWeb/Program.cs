using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Abstract;
using Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "Auto";
    options.LoginPath = "/Login/Login";
    options.AccessDeniedPath = "/Login/Login";
    options.Cookie.IsEssential = true;
});
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error");
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=AnaSayfa}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
