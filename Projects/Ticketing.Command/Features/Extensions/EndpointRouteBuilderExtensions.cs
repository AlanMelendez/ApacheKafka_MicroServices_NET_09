namespace Ticketing.Command.Features.Api
{
    public static class EndpointRouteBuilderExtensions
    {
        
        public static IEndpointRouteBuilder AddAllMinimalApiEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            //Get all class's wich implement "IMinimalApi"
            var listMinimalAPis = routeBuilder.ServiceProvider.GetServices<IMinimalApi>();


            foreach (IMinimalApi minimalApi in listMinimalAPis)
            {
                minimalApi.AddEndpoint(routeBuilder); //Add endpoints definited in entire project.
            }

           // TODO: Generate another extension method to register all classes which implement "IMinimalApi" as services in the container.


            return routeBuilder;
        }
    }
}