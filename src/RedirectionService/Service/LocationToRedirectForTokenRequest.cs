using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    public sealed class LocationToRedirectForTokenRequest
    {
        private readonly static RedirectionOption[] _NoRedirectionOptions = new RedirectionOption[0];

        private readonly string                     _Token;
        private readonly RedirectionOption[]        _Options;

        public LocationToRedirectForTokenRequest(string token)
        {
            _Token   = token;
            _Options = _NoRedirectionOptions;
        }

        public LocationToRedirectForTokenRequest(string token, IEnumerable<RedirectionOption> options)
            : this(token)
        {
            _Options = options.ToArray();
        }

        public string Token
        {
            get { return _Token; }
        }

        public IList<RedirectionOption> Options
        {
            get { return _Options; }
        }
    }
}