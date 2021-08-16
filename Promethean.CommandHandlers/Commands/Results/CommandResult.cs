using System.Collections.Generic;
using System.Net;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.Notifications;
using Promethean.Notifications.Messages.Contracts;

namespace Promethean.CommandHandlers.Commands.Results
{
	public class CommandResult : Notifiable, ICommandResult
	{
		public CommandResult() => Code = HttpStatusCode.InternalServerError;
		public CommandResult(HttpStatusCode code, IReadOnlyDictionary<string, IReadOnlyCollection<INotificationMessage>> notifications = null)
		{
			Code = code;
			AddNotifications(notifications);
		}

		public HttpStatusCode Code { get; private set; }

		void ICommandResult.Populate(HttpStatusCode code, IReadOnlyDictionary<string, IReadOnlyCollection<INotificationMessage>> notifications)
		{
			Code = code;
			AddNotifications(notifications);
		}
	}
}