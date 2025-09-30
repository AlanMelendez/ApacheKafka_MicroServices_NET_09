using MongoDB.Driver;

namespace Ticketing.Command.Domain.Abstracts
{
    public interface ISession
    {
        Task<IClientSessionHandle> BeginSessionAsync(CancellationToken cancellationToken = default);
        void BeginTransaction(IClientSessionHandle session); // To start a transaction
        Task CommitTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default); // To modify data
        Task RollBackTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default);
    
        void DisposeSession(IClientSessionHandle session);
    }
}
