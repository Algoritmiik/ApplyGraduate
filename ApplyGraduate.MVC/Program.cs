using ApplyGraduate.Services.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();

builder.Services.AddSession();

builder.Services.LoadMyServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

bool.TryParse(builder.Configuration?.GetSection("Login")["CookieSecure"], out bool cookieSecure);

app.UseSession();

app.UseCors("PortalPolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();