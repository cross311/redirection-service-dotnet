namespace RedirectionService
{
    public sealed class RedirectionServiceFactory
    {

        public IRedirectionService Build()
        {
            var inMemoryRedirectionRepository = new InMemoryRedirectionRepository();
            var redirectionRepository = new CaseInsensitiveTokenRedirectionRepository(inMemoryRedirectionRepository);
            var tokenBasedRedirectionService = new TokenBasedRedirectionService(redirectionRepository);
            var redirectionService = new LanguageBasedRedirectionService(tokenBasedRedirectionService);
            return redirectionService;
        }
    }
}