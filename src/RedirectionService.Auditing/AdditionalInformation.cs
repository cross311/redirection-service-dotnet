using System;
using System.Collections.Generic;
using System.Linq;

namespace RedirectionService.Auditing
{
    public class AdditionalInformation
    {
        private readonly string _Name;
        private readonly string _Value;

        public AdditionalInformation(string name, string value)
        {
            _Name  = name;
            _Value = value;
        }

        public string Name  { get { return _Name;  } } 
        public string Value { get { return _Value; } } 

        public override bool Equals(object obj)
        {
            var result = Equals(obj as AdditionalInformation);
            return result;
        }

        public bool Equals(AdditionalInformation other)
        {
            if (ReferenceEquals(other, null))
                return false;

            var result =
                _Name == other._Name &&
                _Value == other._Value;

            return result;
        }

        public override int GetHashCode()
        {
            var result =
                _Name.GetHashCode() ^
                _Value.GetHashCode();

            return result;
        }

        // optional operators
        public static Boolean operator ==(AdditionalInformation target, AdditionalInformation other)
        {
            if (ReferenceEquals(target, other))
                return true;

            if (ReferenceEquals(target, null))
                return false;

            var result = target.Equals(other);
            return result;
        }

        public static Boolean operator !=(AdditionalInformation target, AdditionalInformation other)
        {
            var result = !(target == other);
            return result;
        }

        public static readonly IEnumerable<AdditionalInformation> EmptyAdditionalInformations =
            Enumerable.Empty<AdditionalInformation>();
    }
}