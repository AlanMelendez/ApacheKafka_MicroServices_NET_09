
using System.Reflection;
using Ticketing.Command.Features.Api;

namespace Ticketing.Command.Features.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection RegisterAllMinimalApisLikeServices(
            this IServiceCollection services
        )
        {
            //Return an instance of type Assembly that represents the assembly 
            // that contains the code that is currently executing. (Representation of all components in the current project)    
            var currentAssembly = Assembly.GetExecutingAssembly();


            var componentsMinimalApis = currentAssembly
                .GetTypes()
                .Where(type => typeof(IMinimalApi).IsAssignableFrom(type) //Return all classes which implement "IMinimalApi" -> "IMinimalApi", "TicketCreate"
                    && type != typeof(IMinimalApi) //That avoids returning "IMinmalApi" lol
                    && type.IsPublic && !type.IsAbstract //we needs only public classes and avoid abstract classes
                ); 

            //Register all services like services singleton
            foreach(var minimalApi in componentsMinimalApis)
            {
                //It's very important to declarate the type, in this case, it is "IMinimalApi"
                services.AddSingleton(typeof(IMinimalApi), minimalApi);
            }
            
            


            return services;
        }
        
        
    }
}