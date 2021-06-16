using Promethean.CommandHandlers.Commands;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.Notifications;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Handlers.Contracts
{
	public interface ICommandHandler<TCommand, TCommandResult> : INotifiable
		where TCommand : ICommand
		where TCommandResult : ICommandResult
	{
		TCommandResult Handle(TCommand command);
	}
}