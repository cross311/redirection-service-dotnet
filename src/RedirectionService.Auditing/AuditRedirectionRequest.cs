namespace RedirectionService.Auditing
{
    public class AuditRedirectionRequest
    {
        private readonly string _Action;
        private readonly string _Token;
        private readonly string _Location;
        private readonly string _ActorIp;
        private readonly string _Actor;

        public AuditRedirectionRequest(
            string action,
            string token,
            string location,
            string actorIp,
            string actor)
        {
            _Action   = action;
            _Token    = token;
            _Location = location;
            _ActorIp  = actorIp;
            _Actor    = actor;
        }

        public string Action   { get { return _Action;   } } 
        public string Token    { get { return _Token;    } } 
        public string Location { get { return _Location; } } 
        public string ActorIp  { get { return _ActorIp;  } } 
        public string Actor    { get { return _Actor;    } } 
    }
}