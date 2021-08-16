using System.Net;
using System.Threading.Tasks;
using Promethean.CommandHandlers.Tests.Commands;
using Promethean.CommandHandlers.Tests.Commands.Results;
using Promethean.Notifications.Messages;

namespace Promethean.CommandHandlers.Tests.Handlers
{
	public class TestCommandHandler : BaseTestCommandHandler
	{
		public override TestCommandResult Handle(SuccessTestCommand command) => new TestCommandResult(HttpStatusCode.OK, Notifications);

		public override TestCommandResult Handle(FailureTestCommand command) => new TestCommandResult(HttpStatusCode.InternalServerError, Notifications);

		public override TestCommandResult Handle(InvalidTestCommand command)
		{
			AddNotification(nameof(command), NotificationMessage.Invalid);

			return null;
		}

		public override async Task<TestCommandResult> Handle(AsyncSuccessTestCommand command) => await Task.Run(() => new TestCommandResult(HttpStatusCode.OK, Notifications));

		public override async Task<TestCommandResult> Handle(AsyncFailureTestCommand command) => await Task.Run(() => new TestCommandResult(HttpStatusCode.InternalServerError, Notifications));
	}
}