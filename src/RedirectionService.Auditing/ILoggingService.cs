using System.Threading.Tasks;

namespace RedirectionService.Auditing
{
    internal interface ILoggingService
    {
        Task Log(string log);
    }
}