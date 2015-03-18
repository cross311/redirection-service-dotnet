namespace RedirectionService.Auditing
{
    public interface IAuditService
    {
        Audit AuditRedirection(AuditRedirectionRequest request);
    }
}