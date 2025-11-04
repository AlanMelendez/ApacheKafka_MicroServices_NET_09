using AutoMapper;
using Common.Core.Events;
using FluentValidation;
using MediatR;
using MongoDB.Driver;
using Ticketing.Command.Domain.Abstracts;
using Ticketing.Command.Domain.EventModels;
using Ticketing.Command.Features.Api;

namespace Ticketing.Command.Features.Tickets
{

    public class TicketCreate : IMinimalApi
    {
         public void AddEndpoint(IEndpointRouteBuilder routeBuilder)
        {
            //Here add definitions of the endpoints for the minimal API (e.g., POST, GET, etc. methods)
            throw new NotImplementedException();
        }

        //Create class with init properties and don't allow inheritance with sealed
        public sealed class TicketCreateRequest(string username, string typeError, string detailError)
        {
            public string Username { get; set; } = username;
            public string TypeError { get; set; } = typeError;
            public string DetailError { get; set; } = detailError;
        }

        //Using record to create an immutable object to represent the command and implement IRequest from MediatR to indicate that this command will return a boolean value
        public record TicketCreateCommand(TicketCreateRequest ticketCreateRequest) : IRequest<bool>;


        /** Validator class for the TicketCreateCommand class
         * It uses the TicketCreateValidator to validate the ticketCreateRequest property
         **/
        public class TicketCreateCommandValidator : AbstractValidator<TicketCreateCommand>
        {
            public TicketCreateCommandValidator()
            {
                RuleFor(validation => validation.ticketCreateRequest).SetValidator(new TicketCreateValidator());
            }
        }


        public class TicketCreateValidator : AbstractValidator<TicketCreateRequest>
        {

            public TicketCreateValidator()
            {
                RuleFor(rule => rule.Username).NotEmpty().WithMessage("Please insert an username.");
                RuleFor(rule => rule.DetailError).NotEmpty().WithMessage("Please introduce a message detail error.");
            }
        }

        public sealed class TicketCreateCommandHandler(
            IEventModelRepository eventModelRepository,
            IMapper mapper
        ) : IRequestHandler<TicketCreateCommand, bool>
        {
            private readonly IEventModelRepository _eventModelRepository = eventModelRepository;
            private readonly IMapper _mapper = mapper;

            public async Task<bool> Handle(TicketCreateCommand request, CancellationToken cancellationToken)
            {
                var ticketEventData = _mapper.Map<TicketCreatedEvent>(request.ticketCreateRequest);

                var eventModel = new EventModel
                {
                    AggregateIdentifier = Guid.CreateVersion7(DateTimeOffset.UtcNow).ToString(),
                    AggregateType = "TicketAggregate",
                    EventType = "TicketCreated",
                    TimeStamp = DateTime.UtcNow,
                    Version = "1.0",
                    EventData = ticketEventData
                };

                //Open connection to MongoDB and insert the eventModel object
                IClientSessionHandle session = await _eventModelRepository.BeginSessionAsync(cancellationToken);

                try
                {
                    //First init the transaction
                    _eventModelRepository.BeginTransaction(session);

                    //Insert the eventModel object in the database
                    await _eventModelRepository.InsertOneAsync(eventModel, session, cancellationToken);

                    //If all operations are correct, commit the transaction
                    await _eventModelRepository.CommitTransactionAsync(session, cancellationToken);


                    return true;
                }
                catch (Exception)
                {
                    // If occurs an error, rollback all transactions and close the connection from the database.
                    await _eventModelRepository.RollBackTransactionAsync(session, cancellationToken);

                    return false;
                }
                finally
                {
                    // after close the connection, delete the obj session to free memory.
                    _eventModelRepository.DisposeSession(session);
                }
            }
        }

       
    }
}
