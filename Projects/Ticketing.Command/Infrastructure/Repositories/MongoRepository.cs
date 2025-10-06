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
                .GetDatabase(options.Value.Database) // Database name
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
            return _collection.AsQueryable();
        }

        public Task<IClientSessionHandle> BeginSessionAsync(CancellationToken cancellationToken = default)
        {
            var option = new ClientSessionOptions();
            option.DefaultTransactionOptions = new TransactionOptions();
            return _collection.Database.Client.StartSessionAsync(option, cancellationToken);


        }

        public void BeginTransaction(IClientSessionHandle session) => session.StartTransaction();

        public Task CommitTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default) => session.CommitTransactionAsync(cancellationToken);
        

        public void DisposeSession(IClientSessionHandle session) => session.Dispose();
        

        public async Task InsertOneAsync(TDocument document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation)
        {
            await _collection.InsertOneAsync(clientSesionHandle, document, cancellationToken: cancellation);
        }

        public Task RollBackTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default) => session.AbortTransactionAsync();
    }
}
