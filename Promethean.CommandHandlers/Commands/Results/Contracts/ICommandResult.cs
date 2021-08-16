using System.Collections.Generic;
using System.Net;
using Promethean.Notifications.Contracts;
using Promethean.Notifications.Messages.Contracts;

namespace Promethean.CommandHandlers.Commands.Results.Contracts
{
	public interface ICommandResult : INotifiable
	{
		HttpStatusCode Code { get; }

		internal void Populate(HttpStatusCode code, IReadOnlyDictionary<string, IReadOnlyCollection<INotificationMessage>> notifications);
	}
}