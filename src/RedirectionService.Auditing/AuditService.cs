using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedirectionService.Auditing
{
    internal sealed class AuditService : IAuditService
    {
        public Audit AuditRedirection(AuditRedirectionRequest request)
        {
            if(ReferenceEquals(request, null)) throw new ArgumentNullException("request");


            var additionalInformation = new[]
            {
                new AdditionalInformation("token", request.Token),
                new AdditionalInformation("location", request.Location)
            };
            var created = DateTime.UtcNow;

            var audit = new Audit(
                string.Format("redirection.{0}", request.Action),
                request.ActorIp,
                request.Actor,
                additionalInformation,
                created);

            return audit;
        }
    }
}
