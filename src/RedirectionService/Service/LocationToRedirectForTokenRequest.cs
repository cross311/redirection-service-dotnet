using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    public sealed class LocationToRedirectForTokenRequest
    {

        private readonly string                     _Token;

        public LocationToRedirectForTokenRequest(string token)
        {
            _Token   = token;
        }

        public string Token
        {
            get { return _Token; }
        }
    }
}