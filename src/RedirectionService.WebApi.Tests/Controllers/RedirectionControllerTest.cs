using System;
using System.Net;
using System.Threading;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedirectionService.WebApi;
using RedirectionService.WebApi.Controllers;
using Rhino.Mocks;

namespace RedirectionService.WebApi.Tests.Controllers
{
    [TestClass]
    public class RedirectionControllerTest
    {
        [TestMethod]
        public void Get_HappyPath()
        {
            // Arrange
            var token = "token";
            var location = "http://location.com";

            var redirectionService = MockRepository.GenerateStub<IRedirectionService>();
            var controller = new RedirectionController(redirectionService);

            redirectionService.Expect(
                m =>
                    m.LocationToRedirectForToken(
                        Arg<LocationToRedirectForTokenRequest>.Matches(r => r.Token.Equals(token))))
                .Return(new Redirection(token, location));

            // Act
            var result = controller.Get(token);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.Web.Http.Results.RedirectResult));
            Assert.AreEqual(new Uri(location), (result as System.Web.Http.Results.RedirectResult).Location);
        }

        [TestMethod]
        public void Get_NotFound()
        {
            // Arrange
            var token = "token";

            var redirectionService = MockRepository.GenerateStub<IRedirectionService>();
            var controller = new RedirectionController(redirectionService);

            redirectionService.Expect(
                m =>
                    m.LocationToRedirectForToken(Arg<LocationToRedirectForTokenRequest>.Is.Anything))
                .Return(Redirection.Null);

            // Act
            var result = controller.Get(token);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_HappyPath()
        {
            // Arrange
            var token = "token";
            var location = "http://location.com";

            var redirectionService = MockRepository.GenerateStub<IRedirectionService>();
            var controller = new RedirectionController(redirectionService);

            redirectionService.Expect(
                m =>
                    m.ForTokenRedirectToLocation(
                        Arg<ForTokenRedirectToLocationRequest>.Matches(r => r.Token.Equals(token) && r.Location.Equals(location))))
                .Return(new Redirection(token, location));

            // Act
            var result = controller.Post(token, location);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(token, result.Token);
            Assert.AreEqual(location, result.Location);
        }
    }
}
