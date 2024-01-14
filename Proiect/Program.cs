using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CarContext>(options =>
                options.UseSqlServer("Server=DESKTOP-QCOH0UC;Database=Car;Trusted_Connection=True;TrustServerCertificate=true;"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();
var app = builder.Build();
app.UseSession();
app.Use(async (context, next) =>
{
    var userId = context.Session.GetInt32("UserId");
    // If the user is trying to access a page other than Login and is not authenticated
    if ((!context.Request.Path.Equals("/Home", StringComparison.OrdinalIgnoreCase) && !context.Request.Path.Equals("/Login", StringComparison.OrdinalIgnoreCase) && !context.Request.Path.Equals("/Register", StringComparison.OrdinalIgnoreCase)) && userId == null)
    {
        // Redirect them to the Login page
        context.Response.Redirect("/Home");
    }
    else
    {
        // Proceed to the next middleware
        await next();
    }
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
