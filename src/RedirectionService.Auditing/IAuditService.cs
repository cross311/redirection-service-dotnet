namespace RedirectionService.Auditing
{
    public interface IAuditService
    {
        Audit Audit(AuditRequest request);
    }
}