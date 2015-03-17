using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    public sealed class AssignLocationToRedirectionTokenRequest
    {
        private readonly string                     _Token;
        private readonly string                     _Location;

        public AssignLocationToRedirectionTokenRequest(string token, string location)
        {
            _Token    = token;
            _Location = location;
        }

        public string Token
        {
            get { return _Token; }
        }

        public string Location
        {
            get { return _Location; }
        }
    }
}