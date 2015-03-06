using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedirectionService.Test
{
    [TestClass]
    public class RedirectionServiceShouldHandleLanguageBasedRedirection
    {
        private IRedirectionService _RedirectionService;

        [TestInitialize]
        public void TestInitialize()
        {
            _RedirectionService = new RedirectionServiceFactory().Build();
        }

        [TestMethod]
        public void HavingDifferentRedirectionLocationsForDifferentLanguages()
        {
            //arrange
            var token = @"test_token";
            var englishLanguage = @"eng";
            var japaneseLanguage = @"jpn";
            var englishLocation = @"http://www.test_token_redirection_location.com?lang=eng";
            var japaneseLocation = @"http://www.test_token_redirection_location.com?lang=jpn";
            var languageRedirectionOption = "lang";

            var englishForTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: englishLocation,
                                                        options: new[] { new RedirectionOption(languageRedirectionOption, englishLanguage) });
            var japaneseForTokenRedirectToLocationRequest = new ForTokenRedirectToLocationRequest(
                                                        token: token,
                                                        location: japaneseLocation,
                                                        options: new[]{new RedirectionOption(languageRedirectionOption, japaneseLanguage)});

            _RedirectionService.ForTokenRedirectToLocation(englishForTokenRedirectToLocationRequest);
            _RedirectionService.ForTokenRedirectToLocation(japaneseForTokenRedirectToLocationRequest);

            var englishLocationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(
                                                        token: token,
                                                        options: new[] { new RedirectionOption(languageRedirectionOption, englishLanguage) });
            var japaneseLocationToRedirectForTokenRequest = new LocationToRedirectForTokenRequest(
                                                        token: token,
                                                        options: new[] { new RedirectionOption(languageRedirectionOption, japaneseLanguage) });

            // act
            var englishRedirection = _RedirectionService.LocationToRedirectForToken(englishLocationToRedirectForTokenRequest);
            var japaneseRedirection = _RedirectionService.LocationToRedirectForToken(japaneseLocationToRedirectForTokenRequest);

            // assert
            englishRedirection.Location.Should().Be(englishLocation);
            japaneseRedirection.Location.Should().Be(japaneseLocation);

        }
    }
}
