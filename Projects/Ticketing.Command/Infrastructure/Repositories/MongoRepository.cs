using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ticketing.Command.Application.Models;
using Ticketing.Command.Domain.Abstracts;
using Ticketing.Command.Domain.Extensions;
using Ticketing.Command.Domain.Interfaces;

namespace Ticketing.Command.Infrastructure.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {

        //Define table name using to connect with mongo db
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoClient mongoClient, IOptions<MongoSettings> options)
        {
            _collection = mongoClient
                .GetDatabase(options.Value.DatabaseName) // Database name
                .GetCollection<TDocument>(GetCollectionName(
                    typeof(TDocument) //Get collection name from document class name dynamically
                 )); 
        }

        private protected string GetCollectionName(Type documentType)
        {
            var nameCollection = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                 .FirstOrDefault();

            return nameCollection == null ? throw new ArgumentException("Collection name not found") : (nameCollection as BsonCollectionAttribute)!.tableName;
        }

        public IQueryable<TDocument> AsQueryable()
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

        public Task<TDocument> InsertOneAsync(TDocument document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public Task RollBackTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
