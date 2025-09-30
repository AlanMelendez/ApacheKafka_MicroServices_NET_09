
namespace Common.Core.Events
{
    public class TicketCreatedEvent : BaseEvent
    {

        // Pass the event type name to the base class constructor
        public TicketCreatedEvent() : base(nameof(TicketCreatedEvent)) { }
       
        public required string UserName { get; set; }
        public string TypeError { get; set; } = string.Empty;
        public required string DetailError { get; set; }
    }
}
