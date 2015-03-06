namespace RedirectionService
{
    internal sealed class TokenBasedRedirectionService : IRedirectionService
    {
        private readonly IRedirectionRepository _RedirectionRepository;

        public TokenBasedRedirectionService(IRedirectionRepository redirectionRepository)
        {
            _RedirectionRepository = redirectionRepository;
        }

        public Redirection ForTokenRedirectToLocation(ForTokenRedirectToLocationRequest forTokenRedirectToLocationRequest)
        {
            var token       = forTokenRedirectToLocationRequest.Token;
            var location    = forTokenRedirectToLocationRequest.Location;
            var redirection = new Redirection(token, location);

            redirection     = _RedirectionRepository.SaveRedirection(redirection);

            return redirection;
        }

        public Redirection LocationToRedirectForToken(LocationToRedirectForTokenRequest locationToRedirectForTokenRequest)
        {
            var token       = locationToRedirectForTokenRequest.Token;
            var redirection = _RedirectionRepository.GetRedirectionForToken(token);

            return redirection;
        }
    }
}