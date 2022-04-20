using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoolBooks.Data;
using Microsoft.AspNetCore.Identity;
using CoolBooks.Models;
using CoolBooks.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CoolBooksContext");

builder.Services.AddDbContext<CoolBooksContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<test, LikeDislike>();

//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<CoolBooksContext>();

builder.Services.AddDefaultIdentity<CoolBooksUser>(options =>  options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CoolBooksContext>();




builder.Services.ConfigureApplicationCookie(options => 
{
    options.AccessDeniedPath = "/account/login";
    options.LoginPath = "/account/login";
});

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

builder.Services.AddScoped<ITest, Test>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.Run();
