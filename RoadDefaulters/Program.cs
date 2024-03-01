global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using RoadDefaulters.Models;
global using RoadDefaulters.Data;
global using RoadDefaulters.Repositories;
global using RoadDefaulters.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDBContext>(o =>
{
	o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(o => { o.SignIn.RequireConfirmedEmail = false; })
				.AddEntityFrameworkStores<AppDBContext>()
				.AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccount, AccountRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
