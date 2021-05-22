using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.CommandHandlers.Tests.Commands;
using Promethean.CommandHandlers.Tests.Commands.Results;

namespace Promethean.CommandHandlers.Tests.Handlers
{
	[TestClass]
	public class HandlerTests
	{
		private TestCommandHandler _commandHandler;

		[TestInitialize]
		public void Setup() => _commandHandler = new TestCommandHandler();

		[TestMethod("Handle the Success Command, should return OK and have no notifications")]
		public void HandleSuccessCommand()
		{
			TestCommandResult result = _commandHandler.Handle(new SuccessTestCommand());

			Assert.AreEqual(HttpStatusCode.OK, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}

		[TestMethod("Handle the Failure Command, should return Internal Server Error and have no notifications")]
		public void HandleFailureCommand()
		{
			TestCommandResult result = _commandHandler.Handle(new FailureTestCommand());

			Assert.AreEqual(HttpStatusCode.InternalServerError, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}

		[TestMethod("Handle the Async Success Command, should return OK and have no notifications")]
		public async Task HandleAsyncSuccessCommand()
		{
			TestCommandResult result = await _commandHandler.Handle(new AsyncSuccessTestCommand());

			Assert.AreEqual(HttpStatusCode.OK, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}

		[TestMethod("Handle the Async Failure Command, should return Internal Server Error and have no notifications")]
		public async Task HandleAsyncFailureCommand()
		{
			TestCommandResult result = await _commandHandler.Handle(new AsyncFailureTestCommand());

			Assert.AreEqual(HttpStatusCode.InternalServerError, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}
	}
}