namespace RedirectionService
{
    internal interface IRedirectionRepository
    {
        Redirection SaveRedirection(Redirection redirection);
        Redirection GetRedirectionForToken(string token);
    }
}