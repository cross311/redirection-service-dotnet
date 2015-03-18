namespace RedirectionService.Auditing
{
    public class AuditServiceFactory
    {
        public IAuditService Build()
        {
            var auditService = new AuditService();
            return auditService;
        }
    }
}