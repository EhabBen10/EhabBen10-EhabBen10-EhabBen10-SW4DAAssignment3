using MongoDB.Driver;
using Serilog;
using SW4DAAssignment3.Models;

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

    public async Task<List<Binding>> GetLogs(string specificUser, string operation)
    {
        //Make it like it does not matter if it is upper case or lowercase and implement the operation
        var s = _logsCollection.Find(x => x.Properties.LogInfo.specificUser == specificUser);
        var count = s.CountDocumentsAsync();
        Console.WriteLine("number of documents found: " + count);
        return await s.ToListAsync();
    }
}