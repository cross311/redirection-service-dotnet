using System;
using System.Collections.Generic;
using System.Linq;

namespace RedirectionService
{
    internal sealed class LanguageBasedRedirectionService : IRedirectionService
    {
        private const string                 _LanguageOptionKey = @"lang";
        private readonly IRedirectionService _Core;

        public LanguageBasedRedirectionService(IRedirectionService core)
        {
            _Core = core;
        }

        public Redirection ForTokenRedirectToLocation(ForTokenRedirectToLocationRequest forTokenRedirectToLocationRequest)
        {
            var token                     = forTokenRedirectToLocationRequest.Token;
            var location                  = forTokenRedirectToLocationRequest.Location;
            var options                   = forTokenRedirectToLocationRequest.Options;
            var languageToken             = ModifyTokenForLanguageOption(token, options);

            var languageBasedTokenRequest = new ForTokenRedirectToLocationRequest(languageToken, location, options);
            var redirection               = _Core.ForTokenRedirectToLocation(languageBasedTokenRequest);

            return redirection;
        }

        public Redirection LocationToRedirectForToken(LocationToRedirectForTokenRequest locationToRedirectForTokenRequest)
        {
            var token                     = locationToRedirectForTokenRequest.Token;
            var options                   = locationToRedirectForTokenRequest.Options;
            var languageToken             = ModifyTokenForLanguageOption(token, options);

            var languageBasedTokenRequest = new LocationToRedirectForTokenRequest(languageToken, options);
            var redirection               = _Core.LocationToRedirectForToken(languageBasedTokenRequest);

            return redirection;
        }

        private static RedirectionOption GetLanguageOption(IEnumerable<RedirectionOption> redirectionOptions)
        {
            return redirectionOptions.FirstOrDefault(
                (option) => option.Key.Equals(_LanguageOptionKey, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string ModifyTokenForLanguageOption(string token, IEnumerable<RedirectionOption> redirectionOptions)
        {
            var languageOption = GetLanguageOption(redirectionOptions);

            if (!IsValidLanguageOption(languageOption))
                return token;

            var language       = languageOption.Value;
            var languageToken  = CreateLanguageToken(language, token);

            return languageToken;
        }

        private static string CreateLanguageToken(string language, string token)
        {
            const string languageTokenFormat = "language/{0}/{1}";
            var languageToken                = string.Format(languageTokenFormat, language, token);
            return languageToken;
        }

        private static bool IsValidLanguageOption(RedirectionOption languageOption)
        {
            var hasLanguageOption           = !ReferenceEquals(languageOption, null);
            var languageOptionNotWhitespace = hasLanguageOption && !string.IsNullOrWhiteSpace(languageOption.Value);

            return hasLanguageOption && languageOptionNotWhitespace;
        }
    }
}