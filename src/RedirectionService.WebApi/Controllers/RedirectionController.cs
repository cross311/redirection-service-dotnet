using System.Web.Http;

namespace RedirectionService.WebApi.Controllers
{
    public class RedirectionController : ApiController
    {
        private readonly IRedirectionService _RedirectionService;

        public RedirectionController()
            : this(RedirectionServiceConfig.RedirectionService)
        {
        }

        public RedirectionController(IRedirectionService redirectionService)
        {
            _RedirectionService = redirectionService;
        }

        public IHttpActionResult Get(string token)
        {
            var locationToRedirectForTokenRequest = new GetLocationForRedirectionTokenRequest(token);
            var redirection                       = _RedirectionService.GetLocationForRedirectionToken(locationToRedirectForTokenRequest);

            if (redirection == Redirection.Null)
                return NotFound();

            return Redirect(redirection.Location);
        }

        // TODO: Get this to run off a model. need to accept options
        [HttpPost]
        public Redirection Post(string token, string location)
        {
            var forTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(token: token, location: location);
            var savedRedirection                  = _RedirectionService.AssignLocationToRedirectionToken(forTokenRedirectToLocationRequest);

            return savedRedirection;
        }
    }
}
