namespace RedirectionService
{
    internal sealed class TokenBasedRedirectionService : IRedirectionService
    {
        private readonly IRedirectionRepository _RedirectionRepository;

        public TokenBasedRedirectionService(IRedirectionRepository redirectionRepository)
        {
            _RedirectionRepository = redirectionRepository;
        }

        public Redirection AssignLocationToRedirectionToken(AssignLocationToRedirectionTokenRequest assignLocationToRedirectionTokenRequest)
        {
            var token       = assignLocationToRedirectionTokenRequest.Token;
            var location    = assignLocationToRedirectionTokenRequest.Location;
            var redirection = Redirection.Create(token, location);

            redirection     = _RedirectionRepository.SaveRedirection(redirection);

            return redirection;
        }

        public Redirection GetLocationForRedirectionToken(GetLocationForRedirectionTokenRequest getLocationForRedirectionTokenRequest)
        {
            var token       = getLocationForRedirectionTokenRequest.Token;
            var redirection = _RedirectionRepository.GetRedirectionForToken(token);

            return redirection;
        }
    }
}