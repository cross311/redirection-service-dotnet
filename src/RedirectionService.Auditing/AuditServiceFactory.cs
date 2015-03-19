using log4net;
namespace RedirectionService.Auditing
{
    public class AuditServiceFactory
    {
        public IAuditService Build()
        {
            var type          = typeof (IAuditService);
            var log4Net       = LogManager.GetLogger(type);
            var loggingService = new Log4NetLoggingService(log4Net);

            var service       = Build(loggingService);

            return service;
        }

        internal IAuditService Build(ILoggingService loggingService)
        {
            var auditService            = new AuditService();
            var auditServiceWithLogging = new LoggingAuditService(auditService, loggingService);

            return auditServiceWithLogging;
        }
    }
}