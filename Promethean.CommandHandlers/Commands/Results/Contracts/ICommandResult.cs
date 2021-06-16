using System.Net;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Commands.Results.Contracts
{
	public interface ICommandResult : INotifiable
	{
		HttpStatusCode Code { get; }
	}
}