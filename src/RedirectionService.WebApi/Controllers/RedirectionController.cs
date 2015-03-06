using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

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

        // TODO: need to accept options
        public IHttpActionResult Get(string token)
        {
            var locationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(token);
            var redirection = _RedirectionService.LocationToRedirectForToken(locationToRedirectForTokenRequest);

            if (redirection == Redirection.Null)
                return NotFound();

            return Redirect(redirection.Location);
        }

        // TODO: Get this to run off a model. need to accept options
        public Redirection Post(string token, string location)
        {
            var redirection = new Redirection(token, location);
            var forTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(token: redirection.Token, location: redirection.Location);
            var savedRedirection = _RedirectionService.ForTokenRedirectToLocation(forTokenRedirectToLocationRequest);

            return savedRedirection;
        }
    }
}
