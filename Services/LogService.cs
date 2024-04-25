using MongoDB.Driver;

namespace SW4DAAssignment3.Services;

public class LogService
{
    private readonly IMongoCollection<Binding> _logsCollection;

    public LogService(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["Serilog:WriteTo:0:Args:databaseUrl"]);
        var database = client.GetDatabase(configuration["Serilog:WriteTo:0:Args:databaseUrl"].Split('/').Last());
        _logsCollection = database.GetCollection<Binding>(configuration["Serilog:WriteTo:0:Args:collectionName"]);
    }

    public async Task<List<Binding>> GetLogs(string specificUser, string operation, DateTime dateTime)
    {
        var startOfDay = dateTime.Date;
        var endOfDay = startOfDay.AddDays(1);
        var s = _logsCollection.Find(x => x.Properties.LogInfo.specificUser == specificUser.ToLower()
         && x.Properties.LogInfo.Operation.Contains(operation.ToLower()) &&
                    x.Properties.LogInfo.Timestamp >= startOfDay && x.Properties.LogInfo.Timestamp < endOfDay);
        var count = s.CountDocumentsAsync();
        return await s.ToListAsync();
    }
}