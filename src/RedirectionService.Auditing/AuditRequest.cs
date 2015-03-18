using System.Collections.Generic;
using System.Linq;

namespace RedirectionService.Auditing
{
    public class AuditRequest
    {
        private readonly string                       _Action;
        private readonly string                       _ActorIp;
        private readonly string                       _Actor;
        private readonly IList<AdditionalInformation> _AdditionalInformation; 

        public AuditRequest(
            string action,
            string actorIp,
            string actor,
            IEnumerable<AdditionalInformation> additionalInformation)
        {
            _Action                = action;
            _ActorIp               = actorIp;
            _Actor                 = actor;
            _AdditionalInformation = additionalInformation.ToList();
        }

        public string Action                                      { get { return _Action;                } } 
        public string ActorIp                                     { get { return _ActorIp;               } } 
        public string Actor                                       { get { return _Actor;                 } }
        public IList<AdditionalInformation> AdditionalInformation { get { return _AdditionalInformation; } } 
    }
}