using System.Threading.Tasks;
using Promethean.CommandHandlers.Commands;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.Notifications;

namespace Promethean.CommandHandlers.Handlers
{
	public interface ICommandHandler<TCommand, TCommandResult> : INotifiable
		where TCommand : ICommand
		where TCommandResult : ICommandResult
	{
		Task<TCommandResult> Handle(TCommand command);
	}
}