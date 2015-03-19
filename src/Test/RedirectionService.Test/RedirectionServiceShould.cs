using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace RedirectionService.Test
{
    [TestClass]
    public class RedirectionServiceShould
    {
        private IRedirectionService _RedirectionService;

        [TestInitialize]
        public void TestInitialize()
        {
            _RedirectionService = new RedirectionServiceFactory().Build();
        }

        [TestMethod]
        public void CreateARedirectionLocationForARedirectionToken()
        {
            // arrange
            var token                             = @"test_token";
            var location                          = @"http://www.test_token_redirection_location.com";

            var forTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(
                                                        token: token,
                                                        location: location);

            // act
            var redirection = _RedirectionService.AssignLocationToRedirectionToken(forTokenRedirectToLocationRequest);

            //assert
            redirection.Token.Should().Be(token);
            redirection.Location.Should().Be(location);
            redirection.Created.Should().BeCloseTo(DateTime.UtcNow);
            redirection.Updated.Should().Be(redirection.Created);
        }

        [TestMethod]
        public void ReturnRedirectionLocationForRedirectionToken()
        {
            //arrange
            var token                  = @"test_token";
            var location               = @"http://www.test_token_redirection_location.com";

            var forTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(
                                                        token: token,
                                                        location: location);

            _RedirectionService.AssignLocationToRedirectionToken(forTokenRedirectToLocationRequest);

            var locationToRedirectForTokenRequest = new GetLocationForRedirectionTokenRequest(
                                                        token: token);

            // act
            var redirection = _RedirectionService.GetLocationForRedirectionToken(locationToRedirectForTokenRequest);

            // assert
            redirection.Token.Should().Be(token);
            redirection.Location.Should().Be(location);
        }

        [TestMethod]
        public void IgnoreCaseOfTokenWhenReturningRedirectionLocationForRedirectionToken()
        {
            //arrange
            var lowerCaseToken = @"test_token";
            var upperCaseToken = lowerCaseToken.ToUpper();
            var location = @"http://www.test_token_redirection_location.com";

            var forTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(
                                                        token: lowerCaseToken,
                                                        location: location);

            _RedirectionService.AssignLocationToRedirectionToken(forTokenRedirectToLocationRequest);

            var locationToRedirectForTokenRequest = new GetLocationForRedirectionTokenRequest(
                                                        token: upperCaseToken);

            // act
            var redirection = _RedirectionService.GetLocationForRedirectionToken(locationToRedirectForTokenRequest);

            // assert
            redirection.Token.Should().Be(lowerCaseToken);
            redirection.Location.Should().Be(location);
        }

        [TestMethod]
        public void UpdateARedirectionLocationForARedirectionToken()
        {
            // arrange
            var token = @"test_token";
            var location = @"http://www.test_token_redirection_location.com";
            var updatedLocation = @"http://www.test_token_redirection_location.com/update";

            var forTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(
                                                        token: token,
                                                        location: location);
            var updatedForTokenRedirectToLocationRequest = new AssignLocationToRedirectionTokenRequest(
                                                        token: token,
                                                        location: updatedLocation);
            var locationToRedirectForTokenRequest = new GetLocationForRedirectionTokenRequest(token);

            // act
            var intialCreatedRedirection = _RedirectionService.AssignLocationToRedirectionToken(forTokenRedirectToLocationRequest);
            Thread.Sleep(1);
            _RedirectionService.AssignLocationToRedirectionToken(updatedForTokenRedirectToLocationRequest);
            var redirection = _RedirectionService.GetLocationForRedirectionToken(locationToRedirectForTokenRequest);

            //assert
            redirection.Token.Should().Be(token);
            redirection.Location.Should().Be(updatedLocation);
            redirection.Created.Should().Be(intialCreatedRedirection.Created);
            redirection.Updated.Should().BeAfter(redirection.Created);
        }

        [TestMethod]
        public void ReturnNullRedirectionIfRedirectionDoesNotExist()
        {
            // arrange
            var token = @"test_token";
            var locationToRedirectForTokenRequest = new GetLocationForRedirectionTokenRequest(token);

            // act
            var redirection = _RedirectionService.GetLocationForRedirectionToken(locationToRedirectForTokenRequest);

            //assert
            redirection.Should().Be(Redirection.Null);
        }
    }
}
