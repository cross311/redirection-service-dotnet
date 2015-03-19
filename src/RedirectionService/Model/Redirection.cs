using System;

namespace RedirectionService
{
    public sealed class Redirection
    {
        private readonly string _Token;
        private readonly string _Location;
        private readonly DateTime _Created;
        private readonly DateTime _Updated;

        private Redirection(string token, string location, DateTime created, DateTime updated)
        {
            _Token    = token;
            _Location = location;
            _Created  = created;
            _Updated  = updated;

        }

        public string Token     { get { return _Token;    } } 
        public string Location  { get { return _Location; } } 
        public DateTime Created { get { return _Created;  } } 
        public DateTime Updated { get { return _Updated;  } } 

        public static Redirection Create(string token, string location)
        {
            var created     = DateTime.UtcNow;

            var redirection = new Redirection(token, location, created, created);

            return redirection;
        }

        public Redirection UpdateLocation(string location)
        {
            var updated            = DateTime.UtcNow;

            var updatedRedirection = new Redirection(_Token, location, _Created, updated);

            return updatedRedirection;
        }

        public override bool Equals(object obj)
        {
            var result = Equals(obj as Redirection);
            return result;
        }

        public bool Equals(Redirection other)
        {
            if (ReferenceEquals(other, null))
                return false;

            var result =
                _Token == other._Token;

            return result;
        }

        public override int GetHashCode()
        {
            var result =
                _Token.GetHashCode();

            return result;
        }

        // optional operators
        public static Boolean operator ==(Redirection target, Redirection other)
        {
            if (ReferenceEquals(target, other))
                return true;

            if (ReferenceEquals(target, null))
                return false;

            var result = target.Equals(other);
            return result;
        }

        public static Boolean operator !=(Redirection target, Redirection other)
        {
            var result = !(target == other);
            return result;
        }

        // NULL VALUE PATTERN
        public readonly static Redirection Null = Redirection.Create(string.Empty, string.Empty);
    }
}