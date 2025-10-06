using AutoMapper;
using Common.Core.Events;
using Ticketing.Command.Features.Tickets;

namespace Ticketing.Command.Application.Profiles
{
    public class MappingProfile : Profile
    {
        protected MappingProfile() { 
        
            CreateMap<TicketCreate, TicketCreatedEvent>();
        
        }
    }
}
