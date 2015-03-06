using System.Collections.Concurrent;

namespace RedirectionService
{
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
                return Redirection.Null;

            var redirection = _Database[token];

            return redirection;
        }
    }
}