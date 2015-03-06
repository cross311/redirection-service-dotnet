namespace RedirectionService
{
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
}