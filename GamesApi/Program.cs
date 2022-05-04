using GamesApi.Data;
using GamesApi.Extensions;
using GamesApi.Extras;
using GamesApi.Filters;
using GamesApi.Services;
using GamesApi.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();
builder.Services.AddAutoMapper(options => options.AddProfile<AutomapperProfile>());
builder.Services.AddScoped<LiteDbContext>();
builder.Services.AddScoped<FilterHelpers>();
builder.Services.AddScoped<IGamesServices, GamesServices>();
builder.Services.AddScoped<IVehicleServices, VehicleServices>();
builder.Services.AddScoped<ICosmeticsServices, CosmeticsServices>();
builder.Services.AddScoped<IGameCharacterServices, GameCharacterServices>();
builder.Services.AddScoped<ISystemRequirementsServices, SystemRequirementsServices>();
builder.Services.AddScoped<IApiKeyServices, ApiKeyServices>();
builder.Services.AddLiteDb(builder.Configuration.GetConnectionString("LiteDb"));

builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("swagger/v1/swagger.json", "V1");
	options.RoutePrefix = "";
	options.DocumentTitle = "Games Wiki API";
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(opt => opt
	.AllowAnyHeader()
	.AllowCredentials()
	.AllowAnyMethod()
	.SetIsOriginAllowed(_ => true));

app.MapControllers();

app.Run();