using System.Collections.Generic;
using System.Net;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.Notifications.Contracts;

namespace Promethean.CommandHandlers.Tests.Commands.Results
{
	public class TestCommandResult : CommandResult
	{
		public TestCommandResult() { }
		public TestCommandResult(HttpStatusCode code, IReadOnlyCollection<INotification> notifications = null) : base(code, notifications) { }
	}
}