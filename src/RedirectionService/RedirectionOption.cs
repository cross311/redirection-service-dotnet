using System.Collections.Generic;

namespace RedirectionService
{
    public sealed class RedirectionOption
    {
        private readonly KeyValuePair<string, string> _Option;

        public RedirectionOption(string optionKey, string value)
        {
            _Option = new KeyValuePair<string, string>(optionKey, value);
        }

        public string Key   { get { return _Option.Key; } }
        public string Value { get { return _Option.Value; } }

        internal readonly static RedirectionOption Empty = new RedirectionOption(string.Empty,string.Empty);
    }
}