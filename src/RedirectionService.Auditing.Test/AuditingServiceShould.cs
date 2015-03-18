using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace RedirectionService.Auditing.Test
{
    [TestClass]
    public class AuditingServiceShould
    {
        private IAuditService _AuditService;

        [TestInitialize]
        public void TestInitialize()
        {
            _AuditService = new AuditServiceFactory().Build();
        }

        [TestMethod]
        public void AuditActionsAgainstRedirections()
        {
            // arrange

            var action                  = "redirection.retrieve";
            var token                   = "token";
            var location                = "https://location.com";
            var actorIp                 = "192.168.0.1";
            var actor                   = "cross@mdsol.com";
            var additionalInformations  = new[]
                                            {
                                                new AdditionalInformation("token", token),
                                                new AdditionalInformation("location", location)
                                            };

            var auditRequest = new AuditRequest(action, actorIp, actor, additionalInformations);

            // act
            var audit = _AuditService.Audit(auditRequest);

            // assert

            audit.Should().NotBeNull();
            audit.Action.Should().Be("redirection.retrieve");
            audit.Actor.Should().Be(actor);
            audit.ActorIp.Should().Be(actorIp);
            audit.Created.Should().BeCloseTo(DateTime.UtcNow);

            audit.AdditionalInformation.Should().Contain(additionalInformations);
        }
    }
}
