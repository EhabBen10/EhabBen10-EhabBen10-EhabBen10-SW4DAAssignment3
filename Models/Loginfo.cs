using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SW4DAAssignment3.Models;

namespace SW4DAAssignment3.Models
{
    [BsonIgnoreExtraElements]
    public class Loginfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId LoginfoId { get; set; }
        public string? specificUser { get; set; }
        public string? Operation { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

[BsonIgnoreExtraElements]
public class Properties
{
    public Loginfo LogInfo { get; set; }
}

[BsonIgnoreExtraElements]
public class Binding
{
    public Properties Properties { get; set; }
}