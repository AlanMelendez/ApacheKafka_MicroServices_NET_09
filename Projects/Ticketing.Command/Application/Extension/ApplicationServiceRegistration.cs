using FluentValidation;
using Ticketing.Command.Application.Models;
using Ticketing.Command.Application.Profiles;

namespace Ticketing.Command.Application.Extension
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            //Map MongoDbSettings (Model) from credentials in appsetings.json:  Use a className because have the same name of the class within the file.
            services.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings)));


            //Configure MediatR to search (handle refs: IRequestHandler,INotificationHandler) in entire proyect.
            services.AddMediatR(cfg => { 
                cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
            });


            //Register all Validations from (Fluent Validation)
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);


            //Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
