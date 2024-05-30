using AgoraphobiaAPI.Data;
using AgoraphobiaAPI.Interfaces;
using AgoraphobiaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agoraphobia API", Description = "It's high time you got started with that book...", Version = "v1" });
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddDbContext<ApplicationDBContext>(options => {
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IWeaponRepository, WeaponRepository>();
builder.Services.AddScoped<IArmorRepository, ArmorRepository>();
builder.Services.AddScoped<IConsumableRepository, ConsumableRepository>();
builder.Services.AddScoped<IArmorInventoryRepository, ArmorInventoryRepository>();
builder.Services.AddScoped<IWeaponInventoryRepository, WeaponInventoryRepository>();
builder.Services.AddScoped<IConsumableInventoryRepository, ConsumableInventoryRepository>();
builder.Services.AddScoped<IEnemyRepository, EnemyRepository>();
builder.Services.AddScoped<IArmorDroprateRepository, ArmorDroprateRepository>();
builder.Services.AddScoped<IWeaponDroprateRepository, WeaponDroprateRepository>();
builder.Services.AddScoped<IConsumableDroprateRepository, ConsumableDroprateRepository>();
builder.Services.AddScoped<IArmorLootRepository, ArmorLootRepository>();
builder.Services.AddScoped<IWeaponLootRepository, WeaponLootRepository>();
builder.Services.AddScoped<IConsumableLootRepository, ConsumableLootRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IArmorSaleRepository, ArmorSaleRepository>();
builder.Services.AddScoped<IWeaponSaleRepository, WeaponSaleRepository>();
builder.Services.AddScoped<IConsumableSaleRepository, ConsumableSaleRepository>();
builder.Services.AddScoped<IRoomEnemyStatusRepository, RoomEnemyStatusRepository>();
builder.Services.AddScoped<IRoomArmorLootStatusRepository, RoomArmorLootStatusRepository>();
builder.Services.AddScoped<IRoomWeaponLootStatusRepository, RoomWeaponLootStatusRepository>();
builder.Services.AddScoped<IRoomConsumableLootStatusRepository, RoomConsumableLootStatusRepository>();
builder.Services.AddScoped<IRoomMerchantArmorSaleStatusRepository, RoomMerchantArmorSaleStatusRepository>();
builder.Services.AddScoped<IEffectRepository, EffectRepository>();

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