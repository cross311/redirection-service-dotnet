using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Newtonsoft.Json;

namespace RedirectionService.Auditing.Test
{
    [TestClass]
    public class AuditingServiceShould
    {
        [TestMethod]
        public void AuditActionsAgainstRedirections()
        {
            // arrange
            var auditService = new AuditServiceFactory().Build();
            var auditRequest = CreateTestAuditRequest();

            // act
            var audit = auditService.Audit(auditRequest);

            // assert
            audit.Should().NotBeNull();
            audit.Action.Should().Be(auditRequest.Action);
            audit.Actor.Should().Be(auditRequest.Actor);
            audit.ActorIp.Should().Be(auditRequest.ActorIp);
            audit.Created.Should().BeCloseTo(DateTime.UtcNow);

            audit.AdditionalInformation.Should().Contain(auditRequest.AdditionalInformation);
        }

        [TestMethod]
        public void LogAudits()
        {
            // arrange
            var loggingService = new MockLoggingService();
            var auditService   = new AuditServiceFactory().Build(loggingService);
            var auditRequest   = CreateTestAuditRequest();

            // act
            auditService.Audit(auditRequest);

            // assert
            loggingService.LogCalled.Should().BeTrue();
        }

        [TestMethod]
        public void LogAuditsInJson()
        {
            // arrange
            var loggingService = new MockLoggingService();
            var auditService   = new AuditServiceFactory().Build(loggingService);
            var auditRequest   = CreateTestAuditRequest();

            // act
            auditService.Audit(auditRequest);

            // assert
            var lastLogMessage = loggingService.LastLog;
            var json           = JsonConvert.DeserializeObject(lastLogMessage);

            json.Should().NotBeNull();
        }

        private static AuditRequest CreateTestAuditRequest()
        {
            var action   = "redirection.retrieve";
            var token    = "token";
            var location = "https://location.com";
            var actorIp  = "192.168.0.1";
            var actor    = "cross@mdsol.com";

            var additionalInformations = new[]
            {
                new AdditionalInformation("token", token),
                new AdditionalInformation("location", location)
            };

            var auditRequest = new AuditRequest(action, actorIp, actor, additionalInformations);
            return auditRequest;
        }

        private sealed class MockLoggingService : ILoggingService
        {
            public bool LogCalled = false;
            public string LastLog = string.Empty;

            public Task Log(string log)
            {
                LogCalled = true;
                LastLog   = log;
                return Task.FromResult<object>(null);
            }
        }
    }
}
