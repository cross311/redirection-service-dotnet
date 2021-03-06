﻿using System;
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

            var forTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: location);

            // act
            var redirection = _RedirectionService.ForTokenRedirectToLocation(forTokenRedirectToLocationRequest);

            //assert
            redirection.Token.Should().Be(token);
            redirection.Location.Should().Be(location);
        }

        [TestMethod]
        public void ReturnRedirectionLocationForRedirectionToken()
        {
            //arrange
            var token                  = @"test_token";
            var location               = @"http://www.test_token_redirection_location.com";

            var forTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: location);

            _RedirectionService.ForTokenRedirectToLocation(forTokenRedirectToLocationRequest);

            var locationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(
                                                        token: token);

            // act
            var redirection = _RedirectionService.LocationToRedirectForToken(locationToRedirectForTokenRequest);

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

            var forTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: lowerCaseToken,
                                                        location: location);

            _RedirectionService.ForTokenRedirectToLocation(forTokenRedirectToLocationRequest);

            var locationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(
                                                        token: upperCaseToken);

            // act
            var redirection = _RedirectionService.LocationToRedirectForToken(locationToRedirectForTokenRequest);

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

            var forTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: location);
            var updatedForTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: updatedLocation);
            var locationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(token);

            // act
            _RedirectionService.ForTokenRedirectToLocation(forTokenRedirectToLocationRequest);
            _RedirectionService.ForTokenRedirectToLocation(updatedForTokenRedirectToLocationRequest);
            var redirection = _RedirectionService.LocationToRedirectForToken(locationToRedirectForTokenRequest);

            //assert
            redirection.Token.Should().Be(token);
            redirection.Location.Should().Be(updatedLocation);
        }

        [TestMethod]
        public void ReturnNullRedirectionIfRedirectionDoesNotExist()
        {
            // arrange
            var token = @"test_token";
            var locationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(token);

            // act
            var redirection = _RedirectionService.LocationToRedirectForToken(locationToRedirectForTokenRequest);

            //assert
            redirection.Should().Be(Redirection.Null);
        }
    }
}
