namespace RedirectionService
{
    public sealed class RedirectionServiceFactory
    {
        public IRedirectionService Build()
        {
            // REPOSITORY
            var inMemoryRedirectionRepository = new InMemoryRedirectionRepository();
            var redirectionRepository         = new CaseInsensitiveTokenRedirectionRepository(inMemoryRedirectionRepository);

            // SERVICE
            var tokenBasedRedirectionService  = new TokenBasedRedirectionService(redirectionRepository);
            var redirectionService            = new LanguageBasedRedirectionService(tokenBasedRedirectionService);

            return redirectionService;
        }
    }
}