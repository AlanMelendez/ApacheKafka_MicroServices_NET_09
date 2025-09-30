using MongoDB.Bson.Serialization.Attributes;
using Ticketing.Command.Domain.Interfaces;
using MongoDB.Bson;

namespace Ticketing.Command.Domain.Class
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id {  get; set; }
    }
}
