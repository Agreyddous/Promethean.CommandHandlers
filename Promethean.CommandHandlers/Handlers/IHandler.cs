using System.Threading.Tasks;
using Promethean.CommandHandlers.Commands;
using Promethean.CommandHandlers.Commands.Results;

namespace Promethean.CommandHandlers.Handlers
{
	public interface IHandler
	{
		Task<TCommandResult> Handle<TCommand, TCommandResult>(TCommand command) where TCommand : ICommand where TCommandResult : ICommandResult, new();
	}
}