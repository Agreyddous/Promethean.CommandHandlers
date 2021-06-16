using System.Collections.Generic;
using System.Net;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Commands.Results.Contracts
{
	public interface ICommandResult : INotifiable
	{
		HttpStatusCode Code { get; }

		internal void Populate(HttpStatusCode code, IReadOnlyCollection<INotification> notifications);
	}
}