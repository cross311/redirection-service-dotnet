using System;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Collections.Generic;

namespace RedirectionService.Auditing
{
    internal sealed class Log4NetLoggingService : ILoggingService
    {
        private readonly ILog _Logger;

        public Log4NetLoggingService(ILog logger)
        {
            _Logger = logger;
        }

        public Task Log(string log)
        {
            var loggingTask = Task.Factory.StartNew(message => _Logger.Info(message), log);

            return loggingTask;
        }
    }

    internal sealed class LoggingAuditService : IAuditService
    {
        private readonly IAuditService   _Core;
        private readonly ILoggingService _LoggingService;

        public LoggingAuditService(IAuditService core, ILoggingService loggingService)
        {
            _LoggingService = loggingService;
            _Core           = core;
        }

        public Audit Audit(AuditRequest request)
        {
            var audit      = _Core.Audit(request);
            var logMessage = BuildLogMessage(audit);

            _LoggingService.Log(logMessage);

            return audit;
        }

        private static string BuildLogMessage(Audit audit)
        {
            var additionalInformationMessage = BuildAdditionalInformationMessage(audit.AdditionalInformation);
            var message =
                string.Format(
                    "{0}'action':'{2}', 'actor':'{3}', 'actor_ip':'{4}', 'created':'{5}', 'additional_infomation':{6}{1}",
                    '{', '}' ,audit.Action, audit.Actor, audit.ActorIp, audit.Created.ToString("o"), additionalInformationMessage);

            return message;
        }

        private static string BuildAdditionalInformationMessage(IList<AdditionalInformation> list)
        {
            var message = new StringBuilder();

            message.Append('[');

            var lastIndex = list.Count - 1;
            for (var index = 0; index <= lastIndex; index++)
            {
                var information = list[index];
                message.Append("{'");
                message.Append(information.Name);
                message.Append("':'");
                message.Append(information.Value);
                message.Append("'}");

                if (index != lastIndex) 
                    message.Append(',');
            }
            message.Append(']');

            var fullMessage = message.ToString();
            return fullMessage;
        }
    }
}