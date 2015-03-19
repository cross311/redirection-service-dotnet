namespace RedirectionService.WebApi.Models
{
    internal sealed class UserRequest
    {
        private readonly string _UserName;
        private readonly string _IpAddress;
        private readonly string _UrlReferrer;

        public UserRequest(string userName, string ipAddress, string urlReferrer)
        {
            _UserName = userName;
            _IpAddress = ipAddress;
            _UrlReferrer = urlReferrer;
        }

        public string UserName
        {
            get { return _UserName; }
        }

        public string IpAddress
        {
            get { return _IpAddress; }
        }

        public string UrlReferrer
        {
            get { return _UrlReferrer; }
        }
    }
}