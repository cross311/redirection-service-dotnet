using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    public sealed class GetLocationForRedirectionTokenRequest
    {

        private readonly string                     _Token;

        public GetLocationForRedirectionTokenRequest(string token)
        {
            _Token   = token;
        }

        public string Token
        {
            get { return _Token; }
        }
    }
}