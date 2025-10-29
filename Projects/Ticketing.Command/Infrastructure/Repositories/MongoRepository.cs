using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ticketing.Command.Application.Models;
using Ticketing.Command.Domain.Abstracts;
using Ticketing.Command.Domain.Extensions;
using Ticketing.Command.Domain.Interfaces;

namespace Ticketing.Command.Infrastructure.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IDocument
    {

        //Define table name using to connect with mongo db
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoClient mongoClient, IOptions<MongoSettings> options)
        {
            _collection = mongoClient
                .GetDatabase(options.Value.Database) // Database name
                .GetCollection<T>(GetCollectionName(
                    typeof(T) //Get collection name from document class name dynamically
                 )); 
        }

        private protected string GetCollectionName(Type documentType)
        {

            /**
             *? Get all atributes from this type document.. later we use "GetCustomAttributes" to filter only BsonCollectionAttribute
             *? BsonCollectionAttribute is a custom attribute that we created in Domain/Extensions/BsonCollectionAttribute.cs it's used to define the collection name for each document
             *? If the document class doesn't have the BsonCollectionAttribute, we throw an exception
             *? Remembet that BsonCollectionAttribute has a property called "tableName" that contains the name of the collection, and it's used to connect with mongo db
             **/
            var nameCollection = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                 .FirstOrDefault();

            return nameCollection == null ? throw new ArgumentException("Collection name not found") : (nameCollection as BsonCollectionAttribute)!.tableName;
        }

        public IQueryable<T> AsQueryable()
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
        

        public async Task InsertOneAsync(T document, IClientSessionHandle clientSesionHandle, CancellationToken cancellation)
        {
            await _collection.InsertOneAsync(clientSesionHandle, document, cancellationToken: cancellation);
        }

        public Task RollBackTransactionAsync(IClientSessionHandle session, CancellationToken cancellationToken = default) => session.AbortTransactionAsync();
    }
}
