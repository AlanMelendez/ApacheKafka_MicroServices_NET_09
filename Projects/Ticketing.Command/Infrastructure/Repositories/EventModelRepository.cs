using MongoDB.Driver;
using Ticketing.Command.Domain.Abstracts;
using Ticketing.Command.Domain.Models;

namespace Ticketing.Command.Infrastructure.Repositories
{
    public class EventModelRepository : IEventModelRepository
    {
        public IQueryable<EventModel> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<IClientSessionHandle> BeginSessionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(IClientSessionHandle session)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void DisposeSession(IClientSessionHandle session)
        {
            throw new NotImplementedException();
        }

        public Task<EventModel> InsertOneAsync(EventModel document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task RollBackTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
