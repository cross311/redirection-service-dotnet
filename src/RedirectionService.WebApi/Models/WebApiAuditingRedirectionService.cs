using System;
using System.Collections.Generic;
using System.Linq;
using RedirectionService.Auditing;

namespace RedirectionService.WebApi.Models
{
    internal sealed class WebApiAuditingRedirectionService : IRedirectionService
    {
        private readonly IRedirectionService    _Core;
        private readonly IAuditService          _AuditService;
        private readonly IUserRequestRepository _UserRequestRepository;

        public WebApiAuditingRedirectionService(
            IRedirectionService core,
            IAuditService auditService,
            IUserRequestRepository userRequestRepository)
        {
            _Core = core;
            _AuditService = auditService;
            _UserRequestRepository = userRequestRepository;
        }

        public Redirection AssignLocationToRedirectionToken(AssignLocationToRedirectionTokenRequest assignLocationToRedirectionTokenRequest)
        {
            var redirection = _Core.AssignLocationToRedirectionToken(assignLocationToRedirectionTokenRequest);
            AuditAssign(redirection);
            return redirection;
        }

        public Redirection GetLocationForRedirectionToken(GetLocationForRedirectionTokenRequest getLocationForRedirectionTokenRequest)
        {
            var redirection = _Core.GetLocationForRedirectionToken(getLocationForRedirectionTokenRequest);
            AuditGet(redirection);
            return redirection;
        }

        private void AuditAssign(Redirection redirection)
        {
            var action = redirection.Created == redirection.Updated
                ? "redirection.create"
                : "redirection.update";

            Audit(action, redirection);
        }

        private void AuditGet(Redirection redirection)
        {
            var action = redirection == Redirection.Null
                ? "redirection.not_found"
                : "redirection.get";

            Audit(action, redirection);
        }

        private void Audit(string action, Redirection redirection)
        {
            var userRequest = _UserRequestRepository.GetUserRequest();

            var additionalInformation = new[]
            {
                new AdditionalInformation("token", redirection.Token),
                new AdditionalInformation("location", redirection.Location),
                new AdditionalInformation("url_referrer", userRequest.UrlReferrer)
            };
            var auditRequest = new AuditRequest(
                action,
                userRequest.IpAddress,
                userRequest.UserName,
                additionalInformation);

            _AuditService.Audit(auditRequest);
        }
    }
}