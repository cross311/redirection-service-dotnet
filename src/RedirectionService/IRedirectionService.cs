using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
            var redirectionRepository = new InMemoryRedirectionRepository();
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
        private readonly string _Token;
        private readonly string _Location;

        public ForTokenRedirectToLocationRequest(string token, string location)
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
    }

    public sealed class LocationToRedirectForTokenRequest
    {
        private readonly string _Token;

        public LocationToRedirectForTokenRequest(string token)
        {
            _Token = token;
        }

        public string Token
        {
            get { return _Token; }
        }
    }
}
