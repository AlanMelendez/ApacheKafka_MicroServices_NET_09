using MongoDB.Driver;
using Ticketing.Command.Domain.Interfaces;

namespace Ticketing.Command.Domain.Abstracts
{
    public interface IMongoRepository<T> : ISession where T : IDocument
    {
        IQueryable<T> AsQueryable(); // Return IQueryable collection of T documents from MongoDB (useful for LINQ queries)
        Task InsertOneAsync(T document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation); // Insert a single document into MongoDB
    }
}
