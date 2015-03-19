using System.Web;

namespace RedirectionService.WebApi.Models
{
    internal sealed class HttpUserRequestRepository : IUserRequestRepository
    {

        public UserRequest GetUserRequest()
        {
            var httpContext    = HttpContext.Current;
            var request        = httpContext.Request;

            var urlReferrerUri = request.UrlReferrer;
            var urlReferrer    = ReferenceEquals(urlReferrerUri, null)
                ? string.Empty
                : urlReferrerUri.ToString();

            var user           = httpContext.User;
            var userName       = ReferenceEquals(user, null)
                ? string.Empty
                : user.Identity.Name;

            var ipAddress      = request.UserHostAddress;

            var userRequest    = new UserRequest(
                userName   : userName,
                ipAddress  : ipAddress,
                urlReferrer: urlReferrer);

            return userRequest;
        }
    }
}