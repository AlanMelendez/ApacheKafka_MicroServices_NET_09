namespace Ticketing.Command.Features.Api
{
    public static class EndpointRouteBuilderExtensions
    {
        /**
            * This method will be used to map all endpoints defined in the entire project.
            * It's necessary register first all classes which implement "IMinimalApi" as services in the container.
            * For that, we have created the extension method "RegisterAllMinimalApisLikeServices" in the file "ServiceCollectionExtensions.cs"
            * In the program.cs file, we have to call that method before calling this one. 
            */
        public static IEndpointRouteBuilder MapAllMinimalApiEndpoints(this IEndpointRouteBuilder routeBuilder)
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