using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.CommandHandlers.DependencyInjection;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.CommandHandlers.Tests.Commands;
using Promethean.CommandHandlers.Tests.Commands.Results;
using Promethean.Logs.DependencyInjection;

namespace Promethean.CommandHandlers.Tests.DependencyInjection
{
	[TestClass]
	public class CommandHandlerExtensionsTests
	{
		private readonly ServiceProvider _serviceProvider;

		private IServiceScope _serviceScope;

		public CommandHandlerExtensionsTests()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddLogService();
			services.AddLogging(configure => configure.AddConsole());
			services.AddCommandHandlers();

			_serviceProvider = services.BuildServiceProvider();
		}

		[TestInitialize]
		public void Setup() => _serviceScope = _serviceProvider.CreateScope();

		[TestMethod("Handle command and result that have a valid handler registered")]
		public async Task HandleValidRegisteredCommandAndResult()
		{
			IHandler handler = _getService<IHandler>();

			TestCommandResult result = await handler.Handle<SuccessTestCommand, TestCommandResult>(new SuccessTestCommand());

			Assert.IsNotNull(result);
			Assert.AreEqual(HttpStatusCode.OK, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}

		[TestMethod("Handle the Invalid Command, should return the Invalid Command Code (In this case, Bad Request) and have one notification")]
		public async Task HandleInvalidCommand()
		{
			IHandler handler = _getService<IHandler>();

			TestCommandResult result = await handler.Handle<InvalidTestCommand, TestCommandResult>(new InvalidTestCommand());

			Assert.IsNotNull(result);
			Assert.AreEqual(HttpStatusCode.BadRequest, result.Code);
			Assert.AreEqual(1, result.Notifications.Count);
		}

		[TestMethod("Handle command and result that have a valid async handler registered")]
		public async Task HandleValidRegisteredAsyncCommandAndResult()
		{
			IHandler handler = _getService<IHandler>();

			TestCommandResult result = await handler.Handle<AsyncSuccessTestCommand, TestCommandResult>(new AsyncSuccessTestCommand());

			Assert.IsNotNull(result);
			Assert.AreEqual(HttpStatusCode.OK, result.Code);
			Assert.AreEqual(0, result.Notifications.Count);
		}

		[TestMethod("Handle command and result that don't have a valid sync or async handler registered, should throw and ArgumentNullException")]
		public async Task HandleNotRegisteredCommandAndResult()
		{
			IHandler handler = _getService<IHandler>();

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await handler.Handle<NoHandlerTestCommand, TestCommandResult>(new NoHandlerTestCommand()));
		}

		private TService _getService<TService>() => _serviceScope.ServiceProvider.GetService<TService>();
	}
}