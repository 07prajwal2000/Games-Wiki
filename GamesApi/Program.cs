using GamesApi.Data;
using GamesApi.Extensions;
using GamesApi.Extras;
using GamesApi.Models;
using GamesApi.Services;
using GamesApi.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<AutomapperProfile>();
});
builder.Services.AddScoped<LiteDbContext>();
builder.Services.AddScoped<IGamesServices, GamesServices>();
builder.Services.AddScoped<ICosmeticsServices, CosmeticsServices>();
builder.Services.AddScoped<IGameCharacterServices, GameCharacterServices>();
builder.Services.AddScoped<ISystemRequirementsServices, SystemRequirementsServices>();
builder.Services.AddScoped<IApiKeyServices, ApiKeyServices>();
builder.Services.AddLiteDb(builder.Configuration.GetConnectionString("LiteDb"));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(corsPolicyBuilder => 
    {
        corsPolicyBuilder.AllowAnyHeader();
        corsPolicyBuilder.AllowAnyMethod();
        corsPolicyBuilder.WithOrigins("http://localhost:3000", "http://localhost:5500");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();