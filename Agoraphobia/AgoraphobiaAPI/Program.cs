using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agoraphobia API", Description = "It's high time you got started with that book...", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDBContext>(options => {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IWeaponRepository, WeaponRepository>();
builder.Services.AddScoped<IArmorRepository, ArmorRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agoraphobia API V1");
   });
}

app.MapControllers();
app.Run();