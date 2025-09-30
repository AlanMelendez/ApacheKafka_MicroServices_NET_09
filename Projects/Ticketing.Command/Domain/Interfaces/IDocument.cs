using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ticketing.Command.Domain.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; }

    }
}
