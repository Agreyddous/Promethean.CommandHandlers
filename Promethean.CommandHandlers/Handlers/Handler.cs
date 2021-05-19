using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promethean.CommandHandlers.Commands;
using Promethean.CommandHandlers.Commands.Results;
using Promethean.Logs.Contracts;
using Promethean.Notifications;

namespace Promethean.CommandHandlers.Handlers
{
	public class Handler : IHandler
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogService _logService;

		public Handler(IServiceProvider serviceProvider, ILogService logService)
		{
			_serviceProvider = serviceProvider;
			_logService = logService;
		}

		public async Task<TCommandResult> Handle<TCommand, TCommandResult>(TCommand command)
			where TCommand : ICommand
			where TCommandResult : ICommandResult, new()
		{
			TCommandResult result = new TCommandResult();

			ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TCommandResult>)) as ICommandHandler<TCommand, TCommandResult>;

			try
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>("Input", nameof(handler.Handle), new { Input = command }, LogLevel.Debug);

				result = await handler.Handle(command);
			}
			catch (Exception exception)
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>(exception, new { Input = command }, LogLevel.Error);
			}
			finally
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>("Output", nameof(handler.Handle), new { Output = result }, LogLevel.Debug);
			}

			return result;
		}
	}
}