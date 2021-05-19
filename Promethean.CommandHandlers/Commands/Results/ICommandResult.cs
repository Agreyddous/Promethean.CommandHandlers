using System.Net;
using Promethean.Notifications;

namespace Promethean.CommandHandlers.Commands.Results
{
	public interface ICommandResult : INotifiable
	{
		HttpStatusCode Code { get; }
	}
}