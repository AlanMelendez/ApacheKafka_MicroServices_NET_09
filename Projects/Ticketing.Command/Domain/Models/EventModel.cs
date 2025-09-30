using Common.Core.Events;
using MongoDB.Bson.Serialization.Attributes;
using Ticketing.Command.Domain.Class;
using Ticketing.Command.Domain.Extensions;

namespace Ticketing.Command.Domain.Models
{
    [BsonCollection("eventStores")]
    public class EventModel<T> : Document
    {
        [BsonElement("timestamp")]
        public DateTime TimeStep { get; set; }

        [BsonElement("aggregateIdentifier")]
        public required string AggregateIdentifier { get; set; }

        [BsonElement("aggregateType")]
        public string AggregateType { get; set; } = string.Empty;

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("eventType")]
        public string EventType { get; set; } = string.Empty;

        public BaseEvent? EventData {get; set;}
    }
}
