using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
using Microsoft.Extensions.Configuration;
using SWD392_FA24_SportShop.AppStarts;
using SWD392_SportShop.AppStarts;
using SWD392_SportShop.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new PayPalService("PayPal:ClientId", "PayPal:ClientSecret"));
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.ConfigureSwaggerServices();

builder.Services.ConfigureAuthService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureAutoMapper();

builder.Services.ServiceContainer(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

//builder.Services.AddSingleton(x =>
//    new PayPalClient(
//        builder.Configuration["PayPal:ClientId"],
//        builder.Configuration["PayPal:ClientSecret"],
//        builder.Configuration["PayPal:Environment"]
//        )
//);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
