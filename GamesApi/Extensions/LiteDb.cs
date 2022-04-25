using LiteDB;

namespace GamesApi.Extensions;

public static class LiteDb
{
    public static void AddLiteDb(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ILiteDatabase, LiteDatabase>(_ => new LiteDatabase(connectionString));
    }
    
}