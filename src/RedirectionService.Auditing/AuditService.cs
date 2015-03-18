using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedirectionService.Auditing
{
    internal sealed class AuditService : IAuditService
    {
        public Audit Audit(AuditRequest request)
        {
            if(ReferenceEquals(request, null)) throw new ArgumentNullException("request");
            var created = DateTime.UtcNow;

            var audit = new Audit(
                request.Action,
                request.ActorIp,
                request.Actor,
                request.AdditionalInformation,
                created);

            return audit;
        }
    }
}
