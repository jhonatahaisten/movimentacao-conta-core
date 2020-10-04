using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Conta
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    public int conta { get; set; }
    public double saldo { get; set; }
}