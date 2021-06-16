using System.Net;
using System.Threading.Tasks;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.CommandHandlers.Tests.Commands;
using Promethean.CommandHandlers.Tests.Commands.Results;
using Promethean.Notifications;
using Promethean.Notifications.Messages;

namespace Promethean.CommandHandlers.Tests.Handlers
{
	public class TestCommandHandler : Notifiable,
		ICommandHandler<SuccessTestCommand, TestCommandResult>,
		ICommandHandler<FailureTestCommand, TestCommandResult>,
		ICommandHandler<InvalidTestCommand, TestCommandResult>,
		IAsyncCommandHandler<AsyncSuccessTestCommand, TestCommandResult>,
		IAsyncCommandHandler<AsyncFailureTestCommand, TestCommandResult>
	{
		public TestCommandResult Handle(SuccessTestCommand command) => new TestCommandResult(HttpStatusCode.OK, Notifications);

		public TestCommandResult Handle(FailureTestCommand command) => new TestCommandResult(HttpStatusCode.InternalServerError, Notifications);

		public TestCommandResult Handle(InvalidTestCommand command)
		{
			AddNotification(nameof(command), NotificationMessage.Invalid);

			return null;
		}

		public async Task<TestCommandResult> Handle(AsyncSuccessTestCommand command) => await Task.Run(() => new TestCommandResult(HttpStatusCode.OK, Notifications));

		public async Task<TestCommandResult> Handle(AsyncFailureTestCommand command) => await Task.Run(() => new TestCommandResult(HttpStatusCode.InternalServerError, Notifications));
	}
}