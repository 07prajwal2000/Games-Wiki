using EFCoreLearning.Extensions;
using EFCoreLearning.Extras;
using EFCoreLearning.Services;
using EFCoreLearning.Services.IServices;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<AutomapperProfile>();
});
builder.Services.AddScoped<IGamesServices, GamesServices>();
builder.Services.AddScoped<IGameCharacterServices, GameCharacterServices>();
builder.Services.AddScoped<ISystemRequirementsServices, SystemRequirementsServices>();
builder.Services.AddLiteDb(builder.Configuration.GetConnectionString("LiteDb"));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(corsPolicyBuilder => {
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