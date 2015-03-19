using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedirectionService.Auditing;
using RedirectionService.WebApi.Models;
using Rhino.Mocks;

namespace RedirectionService.WebApi.Tests.Models
{
    [TestClass]
    public class WebApiAuditingRedirectionServiceShould
    {
        private readonly MockUserRequestRepository _UserRequestRepository = new MockUserRequestRepository();
        private IAuditService _AuditService;
        private IRedirectionService _CoreRedirectionService;
        private IRedirectionService _WebApiAuditingRedirectionService;

        [TestInitialize]
        public void TestInitialize()
        {
            _AuditService                     = MockRepository.GenerateMock<IAuditService>();
            _CoreRedirectionService           = MockRepository.GenerateMock<IRedirectionService>();

            _WebApiAuditingRedirectionService = new WebApiAuditingRedirectionService(
                _CoreRedirectionService,
                _AuditService,
                _UserRequestRepository);
        }


        [TestMethod]
        public void AuditsRedirectionInformation()
        {
            // arrange
            var token = "token";
            var location = "http://location.com/";
            var request = new GetLocationForRedirectionTokenRequest("token");
            _CoreRedirectionService.Expect(
                s => s.GetLocationForRedirectionToken(Arg<GetLocationForRedirectionTokenRequest>.Is.Equal(request)))
                .Return(Redirection.Create(token, location));
            _AuditService.Expect(
                s => s.Audit(Arg<AuditRequest>.Matches(r =>
                    r.Actor.Equals(MockUserRequestRepository.UserRequest.UserName)
                    && r.ActorIp.Equals(MockUserRequestRepository.UserRequest.IpAddress)
                    && r.AdditionalInformation.Contains(new AdditionalInformation("token", token))
                    && r.AdditionalInformation.Contains(new AdditionalInformation("location", location))
                    && r.AdditionalInformation.Contains(new AdditionalInformation("url_referrer", MockUserRequestRepository.UserRequest.UrlReferrer))
                )))
                .Return(null);

            // act
            _WebApiAuditingRedirectionService.GetLocationForRedirectionToken(request);

            // assert
            _AuditService.VerifyAllExpectations();
        }


        [TestMethod]
        public void AuditGetRedirection()
        {
            // arrange
            var token = "token";
            var location = "http://location.com/";
            var request = new GetLocationForRedirectionTokenRequest("token");
            _CoreRedirectionService.Expect(
                s => s.GetLocationForRedirectionToken(Arg<GetLocationForRedirectionTokenRequest>.Is.Equal(request)))
                .Return(Redirection.Create(token, location));
            _AuditService.Expect(
                s => s.Audit(Arg<AuditRequest>.Matches(r =>
                    r.Action.Equals("redirection.get")
                )))
                .Return(null);

            // act
            _WebApiAuditingRedirectionService.GetLocationForRedirectionToken(request);

            // assert
            _CoreRedirectionService.VerifyAllExpectations();
            _AuditService.VerifyAllExpectations();
        }


        [TestMethod]
        public void AuditCreateRedirection()
        {
            // arrange
            var token = "token";
            var location = "http://location.com/";
            var request = new AssignLocationToRedirectionTokenRequest("token", location);
            _CoreRedirectionService.Expect(
                s => s.AssignLocationToRedirectionToken(Arg<AssignLocationToRedirectionTokenRequest>.Is.Equal(request)))
                .Return(Redirection.Create(token, location));
            _AuditService.Expect(
                s => s.Audit(Arg<AuditRequest>.Matches(r =>
                    r.Action.Equals("redirection.create")
                )))
                .Return(null);

            // act
            _WebApiAuditingRedirectionService.AssignLocationToRedirectionToken(request);

            // assert
            _CoreRedirectionService.VerifyAllExpectations();
            _AuditService.VerifyAllExpectations();
        }


        [TestMethod]
        public void AuditUpdateRedirection()
        {
            // arrange
            var token = "token";
            var location = "http://location.com/";
            var request = new AssignLocationToRedirectionTokenRequest("token", location);

            var redirection = Redirection.Create(token, location);
            Thread.Sleep(1);
            redirection = redirection.UpdateLocation(location);

            _CoreRedirectionService.Expect(
                s => s.AssignLocationToRedirectionToken(Arg<AssignLocationToRedirectionTokenRequest>.Is.Equal(request)))
                .Return(redirection);
            _AuditService.Expect(
                s => s.Audit(Arg<AuditRequest>.Matches(r =>
                    r.Action.Equals("redirection.update")
                )))
                .Return(null);

            // act
            _WebApiAuditingRedirectionService.AssignLocationToRedirectionToken(request);

            // assert
            _CoreRedirectionService.VerifyAllExpectations();
            _AuditService.VerifyAllExpectations();
        }



        [TestMethod]
        public void AuditNotFoundRedirection()
        {
            // arrange
            var token = "token";
            var request = new GetLocationForRedirectionTokenRequest("token");
            _CoreRedirectionService.Expect(
                s => s.GetLocationForRedirectionToken(Arg<GetLocationForRedirectionTokenRequest>.Is.Equal(request)))
                .Return(Redirection.Null);
            _AuditService.Expect(
                s => s.Audit(Arg<AuditRequest>.Matches(r =>
                    r.Action.Equals("redirection.not_found")
                )))
                .Return(null);

            // act
            _WebApiAuditingRedirectionService.GetLocationForRedirectionToken(request);

            // assert
            _CoreRedirectionService.VerifyAllExpectations();
            _AuditService.VerifyAllExpectations();
        }

        private sealed class MockUserRequestRepository : IUserRequestRepository
        {
            public static readonly  UserRequest UserRequest = new UserRequest("TestName", "127.0.0.1", "http://localhost/");

            public UserRequest GetUserRequest()
            {
                return UserRequest;
            }
        }
    }
}
