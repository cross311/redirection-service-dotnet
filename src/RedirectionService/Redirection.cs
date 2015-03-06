namespace RedirectionService
{
    public sealed class Redirection
    {
        private readonly string _Token;
        private readonly string _Location;

        public Redirection(string token, string location)
        {
            _Token    = token;
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
}