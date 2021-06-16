using System.Threading.Tasks;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Handlers.Contracts
{
	public interface IAsyncCommandHandler<TCommand, TCommandResult> : INotifiable
		where TCommand : ICommand
		where TCommandResult : ICommandResult
	{
		Task<TCommandResult> Handle(TCommand command);
	}
}