using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectWeb.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProjectWeb.BL.Auth.IAuth, ProjectWeb.BL.Auth.Auth>();
builder.Services.AddSingleton<ProjectWeb.BL.Auth.IEncrypt, ProjectWeb.BL.Auth.Encrypt>();
builder.Services.AddScoped<ProjectWeb.BL.Auth.ICurrentUser, ProjectWeb.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<ProjectWeb.DAL.IAuthDAL, ProjectWeb.DAL.AuthDAL>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ProjectWeb.DAL.IDbSessionDAL, ProjectWeb.DAL.DbSessionDAL>();
builder.Services.AddScoped<ProjectWeb.BL.Auth.IDbSession, ProjectWeb.BL.Auth.DbSession>();
builder.Services.AddScoped<ProjectWeb.BL.General.IWebCookie, ProjectWeb.BL.General.WebCookie>();


builder.Services.AddMvc();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
