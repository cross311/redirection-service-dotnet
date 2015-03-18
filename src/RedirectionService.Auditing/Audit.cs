using System;
using System.Collections.Generic;
using System.Linq;

namespace RedirectionService.Auditing
{
    public class Audit
    {
        private readonly string                       _Action;
        private readonly string                       _ActorIp;
        private readonly string                       _Actor;
        private readonly DateTime                     _Created;
        private readonly IList<AdditionalInformation> _AdditionalInformation; 

        internal Audit(
            string action,
            string actorIp,
            string actor,
            IEnumerable<AdditionalInformation> additionalInformation,
            DateTime created)
        {
            _Action                = action;
            _ActorIp               = actorIp;
            _Actor                 = actor;
            _AdditionalInformation = additionalInformation.ToList();
            _Created               = created;
        }

        public string Action                                      { get { return _Action;                } } 
        public string ActorIp                                     { get { return _ActorIp;               } } 
        public string Actor                                       { get { return _Actor;                 } } 
        public DateTime Created                                   { get { return _Created;               } } 
        public IList<AdditionalInformation> AdditionalInformation { get { return _AdditionalInformation; } } 
    }
}