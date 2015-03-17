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
            var redirectionService = new TokenBasedRedirectionService(redirectionRepository);

            return redirectionService;
        }
    }
}