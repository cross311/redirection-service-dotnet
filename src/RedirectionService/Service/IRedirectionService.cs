using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RedirectionService
{
    public interface IRedirectionService
    {
        Redirection ForTokenRedirectToLocation(ForTokenRedirectToLocationRequest forTokenRedirectToLocationRequest);
        Redirection LocationToRedirectForToken(LocationToRedirectForTokenRequest locationToRedirectForTokenRequest);
    }
}
