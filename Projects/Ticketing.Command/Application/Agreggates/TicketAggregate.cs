namespace Ticketing.Command.Application.Agreggates
{
    using Common.Core.Events;
    using Ticketing.Command.Domain.Abstracts;
    using Ticketing.Command.Domain.EventModels;
    using static Ticketing.Command.Features.Tickets.TicketCreate;

    public  class TicketAggregate : AggregateRoot
    {
        public bool Activated { get; set; } = false;
        public TicketAggregate(TicketCreateCommand command): base()
        {
            var ticketCreatedEvent = new TicketCreatedEvent
            {
                Id = command.Id,
                UserName = command.ticketCreateRequest.Username,
                TypeError = command.ticketCreateRequest.TypeError,
                DetailError = command.ticketCreateRequest.DetailError
            };

            RaiseEvent(ticketCreatedEvent);
            
        }

        public void Apply(TicketCreatedEvent @event)
        {
            _id = @event.Id; // Set the aggregate ID from the event to ensure consistency
            Activated = true;
        }

       
    }
}