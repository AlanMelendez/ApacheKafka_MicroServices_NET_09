using MongoDB.Driver;
using Ticketing.Command.Domain.Interfaces;

namespace Ticketing.Command.Domain.Abstracts
{
    public interface IMongoRepository<TDocument> : ISession where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();
        Task<TDocument> InsertOneAsync(TDocument document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation);

    }
}
