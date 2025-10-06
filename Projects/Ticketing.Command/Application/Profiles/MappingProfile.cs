using AutoMapper;
using Common.Core.Events;
using Ticketing.Command.Features.Tickets;
using static Ticketing.Command.Features.Tickets.TicketCreate;

namespace Ticketing.Command.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        
            CreateMap<TicketCreateRequest, TicketCreatedEvent>();
        
        }
    }
}
