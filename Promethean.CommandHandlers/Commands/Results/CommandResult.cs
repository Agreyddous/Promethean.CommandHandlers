using System.Collections.Generic;
using System.Net;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.Notifications;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Commands.Results
{
	public class CommandResult : Notifiable, ICommandResult
	{
		public CommandResult() => Code = HttpStatusCode.InternalServerError;
		public CommandResult(HttpStatusCode code, IReadOnlyCollection<INotification> notifications = null)
		{
			Code = code;
			AddNotifications(notifications);
		}

		public HttpStatusCode Code { get; private set; }

		void ICommandResult.Populate(HttpStatusCode code, IReadOnlyCollection<INotification> notifications)
		{
			Code = code;
			AddNotifications(notifications);
		}
	}
}