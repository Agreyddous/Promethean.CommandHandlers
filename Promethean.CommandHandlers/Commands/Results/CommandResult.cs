using System.Collections.Generic;
using System.Net;
using Promethean.Notifications;

namespace Promethean.CommandHandlers.Commands.Results
{
	public class CommandResult : Notifiable, ICommandResult
	{
		public CommandResult() => Code = HttpStatusCode.InternalServerError;
		public CommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications = null)
		{
			Code = code;
			AddNotifications(notifications);
		}

		public HttpStatusCode Code { get; private set; }
	}
}