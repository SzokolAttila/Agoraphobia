using AgoraphobiaLibrary;
using Microsoft.OpenApi.Models;
var accounts = new Accounts(new List<Account>());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agoraphobia API", Description = "It's high time you got started with that book...", Version = "v1" });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agoraphobia API V1");
   });
}

app.MapGet("/accounts", () => accounts.GetAccounts());
app.MapGet("/accounts/{id}", (int id) => accounts.GetAccount(id));
app.MapPost("/accounts", (int id, string username, string password) => accounts.CreateAccount(id, username, password));
app.MapPut("/accounts", (Account account) => accounts.UpdateAccount(account));
app.MapDelete("/accounts/{id}", (int id) => accounts.DeleteAccount(id));

app.Run();