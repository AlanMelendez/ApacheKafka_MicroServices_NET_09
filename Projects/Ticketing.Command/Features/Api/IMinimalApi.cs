namespace Ticketing.Command.Features.Api
{
    public interface IMinimalApi
    {
        /// <summary>
        /// Method to add endpoints to the route builder
        /// 
        /// With IEndpointRouteBuilder as parameter, it allows defining routes for minimal APIs.
        /// 
        /// </summary>
        /// <remarks>
        ///  I'ts able to create methods like "GET", "POST", etc., and map them to specific handlers.
        /// </remarks>
        /// <param name="routeBuilder">The route builder to add endpoints to.</param>
        void AddEndpoint(IEndpointRouteBuilder routeBuilder);
    }
}