using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RedirectionService
{
    public interface IRedirectionService
    {
        Redirection ForTokenRedirectToLocation(ForTokenRedirectToLocationRequest forTokenRedirectToLocationRequest);

        Redirection LocationToRedirectForToken(LocationToRedirectForTokenRequest locationToRedirectForTokenRequest);
    }

    public sealed class RedirectionServiceFactory
    {

        public IRedirectionService Build()
        {
            var inMemoryRedirectionRepository = new InMemoryRedirectionRepository();
            var redirectionRepository = new CaseInsensitiveTokenRedirectionRepository(inMemoryRedirectionRepository);
            var redirectionService = new TokenBasedRedirectionService(redirectionRepository);
            return redirectionService;
        }
    }

    internal sealed class TokenBasedRedirectionService : IRedirectionService
    {
        private readonly IRedirectionRepository _RedirectionRepository;

        public TokenBasedRedirectionService(IRedirectionRepository redirectionRepository)
        {
            _RedirectionRepository = redirectionRepository;
        }

        public Redirection ForTokenRedirectToLocation(ForTokenRedirectToLocationRequest forTokenRedirectToLocationRequest)
        {
            var token = forTokenRedirectToLocationRequest.Token;
            var location = forTokenRedirectToLocationRequest.Location;
            var redirection = new Redirection(token, location);

            redirection = _RedirectionRepository.SaveRedirection(redirection);

            return redirection;
        }

        public Redirection LocationToRedirectForToken(LocationToRedirectForTokenRequest locationToRedirectForTokenRequest)
        {
            var token = locationToRedirectForTokenRequest.Token;
            var redirection = _RedirectionRepository.GetRedirectionForToken(token);

            return redirection;
        }
    }

    internal interface IRedirectionRepository
    {
        Redirection SaveRedirection(Redirection redirection);
        Redirection GetRedirectionForToken(string token);
    }

    internal sealed class InMemoryRedirectionRepository : IRedirectionRepository
    {
        private readonly ConcurrentDictionary<string, Redirection> _Database;

        public InMemoryRedirectionRepository()
        {
            _Database = new ConcurrentDictionary<string, Redirection>();
        }

        public Redirection SaveRedirection(Redirection redirection)
        {
            var savedRedirection = _Database.AddOrUpdate(redirection.Token, redirection, (token, oldRedirectino) => redirection);

            return savedRedirection;
        }

        public Redirection GetRedirectionForToken(string token)
        {
            if (!_Database.ContainsKey(token))
                return Redirection.Empty;

            var redirection = _Database[token];

            return redirection;
        }
    }

    internal sealed class CaseInsensitiveTokenRedirectionRepository : IRedirectionRepository
    {
        private readonly IRedirectionRepository _Core;

        public CaseInsensitiveTokenRedirectionRepository(IRedirectionRepository core)
        {
            _Core = core;
        }

        public Redirection SaveRedirection(Redirection redirection)
        {
            var token = redirection.Token;
            var location = redirection.Location;
            var caseInsensitiveToken = MakeTokenCaseInsensitive(token);
            var normalizedRedirection = new Redirection(caseInsensitiveToken, location);

            var savedRedirection = _Core.SaveRedirection(normalizedRedirection);

            return savedRedirection;
        }

        public Redirection GetRedirectionForToken(string token)
        {
            var caseInsensitiveToken = MakeTokenCaseInsensitive(token);

            var redirection = _Core.GetRedirectionForToken(caseInsensitiveToken);
            return redirection;
        }

        private static string MakeTokenCaseInsensitive(string token)
        {
            var caseInsensitiveToken = token.ToLower();
            return caseInsensitiveToken;
        }
    }

    public sealed class Redirection
    {
        private readonly string _Token;
        private readonly string _Location;

        public Redirection(string token, string location)
        {
            _Token = token;
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

        // NULL VALUE PATTERN
        public readonly static Redirection Empty = new Redirection(string.Empty, string.Empty);
    }

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

    public sealed class LocationToRedirectForTokenRequest
    {
        private readonly static RedirectionOption[] _NoRedirectionOptions = new RedirectionOption[0];

        private readonly string _Token;
        private readonly RedirectionOption[] _Options;

        public LocationToRedirectForTokenRequest(string token)
        {
            _Token = token;
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

    public sealed class RedirectionOption
    {
        private readonly KeyValuePair<string, string> _Option;

        public RedirectionOption(string optionKey, string value)
        {
            _Option = new KeyValuePair<string, string>(optionKey, value);
        }

        public string Key   { get { return _Option.Key; } }
        public string Value { get { return _Option.Value; } }

        internal readonly static RedirectionOption Empty = new RedirectionOption(string.Empty,string.Empty);
    }
}
