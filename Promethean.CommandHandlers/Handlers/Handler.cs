using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.Logs.Services.Contracts;

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
			bool async = false;

			IAsyncCommandHandler<TCommand, TCommandResult> asyncHandler = null;
			ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TCommandResult>)) as ICommandHandler<TCommand, TCommandResult>;

			if (handler == null)
			{
				async = true;
				asyncHandler = _serviceProvider.GetService(typeof(IAsyncCommandHandler<TCommand, TCommandResult>)) as IAsyncCommandHandler<TCommand, TCommandResult>;
			}

			try
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>("Input", nameof(handler.Handle), new { Input = command }, LogLevel.Debug);

				if (!async)
					result = handler.Handle(command);

				else
					result = await asyncHandler.Handle(command);
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