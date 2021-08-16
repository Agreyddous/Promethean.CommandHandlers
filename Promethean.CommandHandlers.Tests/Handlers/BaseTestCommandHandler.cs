using System.Threading.Tasks;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.CommandHandlers.Tests.Commands;
using Promethean.CommandHandlers.Tests.Commands.Results;
using Promethean.Notifications;

namespace Promethean.CommandHandlers.Tests.Handlers
{
	public abstract class BaseTestCommandHandler : Notifiable,
		ICommandHandler<SuccessTestCommand, TestCommandResult>,
		ICommandHandler<FailureTestCommand, TestCommandResult>,
		ICommandHandler<InvalidTestCommand, TestCommandResult>,
		IAsyncCommandHandler<AsyncSuccessTestCommand, TestCommandResult>,
		IAsyncCommandHandler<AsyncFailureTestCommand, TestCommandResult>
	{
		public abstract TestCommandResult Handle(SuccessTestCommand command);

		public abstract TestCommandResult Handle(FailureTestCommand command);

		public abstract TestCommandResult Handle(InvalidTestCommand command);

		public abstract Task<TestCommandResult> Handle(AsyncSuccessTestCommand command);

		public abstract Task<TestCommandResult> Handle(AsyncFailureTestCommand command);
	}
}