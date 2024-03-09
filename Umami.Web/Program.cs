using Microsoft.EntityFrameworkCore;
using Umami.Application.Services.Implementations;
using Umami.Application.Services.Interfaces;
using Umami.Application.Services.Interfaces.Services;
using Umami.Infrastructure.Database.Configuration;
using Umami.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStr")));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUmamiPostRepository, UmamiPostRepository>();
//builder.Services.AddScoped<IUmamiPostService, UmamiPostService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
