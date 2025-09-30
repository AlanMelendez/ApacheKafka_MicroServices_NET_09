using Ticketing.Command.Domain.Models;

namespace Ticketing.Command.Domain.Abstracts
{
    public interface IEventModelRepository: IMongoRepository<EventModel>
    {
    }
}
