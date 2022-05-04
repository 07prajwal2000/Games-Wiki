using GamesApi.Data;

namespace GamesApi.Filters;

public class FilterHelpers
{
    private readonly LiteDbContext _context;

    public FilterHelpers(LiteDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateApiKey(Guid key)
    {
        return await Task.Run(() =>
        {
            var apiKey = _context.ApiKeys.Query().Where(x => x.Key == key).FirstOrDefault();
            if (apiKey is null) return false;
            if (apiKey.Blocked || apiKey.ValidTill > DateTime.Now) return false;
            return true;
        });
    }
}