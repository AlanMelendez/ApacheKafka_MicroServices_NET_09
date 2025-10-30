using Common.Core.Events;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Ticketing.Command.Domain.Abstracts;
using Ticketing.Command.Infrastructure.Repositories;
using Ticketing.Command.Domain.EventModels;
namespace Ticketing.Command.Infrastructure.Extension
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //
            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<TicketCreatedEvent>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


            services.AddTransient<IEventModelRepository, EventModelRepository>();
            
            services.AddSingleton<IMongoClient, MongoClient>(
                cfg => new MongoClient(configuration.GetConnectionString("MongoDb"))
            ); // Unique connection to database


            return services;

        }
    }
}
