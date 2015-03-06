using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    public sealed class ForTokenRedirectToLocationRequest
    {
        private readonly static RedirectionOption[] _NoRedirectionOptions = new RedirectionOption[0];

        private readonly string _Token;
        private readonly string _Location;
        private readonly RedirectionOption[] _Options;

        public ForTokenRedirectToLocationRequest(string token, string location)
        {
            _Token = token;
            _Location = location;
            _Options = _NoRedirectionOptions;
        }

        public ForTokenRedirectToLocationRequest(string token, string location, IEnumerable<RedirectionOption> options)
            : this(token, location)
        {
            _Options = options.ToArray();
        }

        public string Token
        {
            get { return _Token; }
        }

        public string Location
        {
            get { return _Location; }
        }

        public IList<RedirectionOption> Options
        {
            get { return _Options; }
        }
    }
}