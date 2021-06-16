using System.Threading.Tasks;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.CommandHandlers.Commands.Results.Contracts;

namespace Promethean.CommandHandlers.Handlers.Contracts
{
	public interface IHandler
	{
		Task<TCommandResult> Handle<TCommand, TCommandResult>(TCommand command) where TCommand : ICommand where TCommandResult : ICommandResult, new();
	}
}