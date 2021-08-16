using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.CommandHandlers.Commands.Results.Contracts;
using Promethean.CommandHandlers.Enums;
using Promethean.CommandHandlers.Handlers.Contracts;
using Promethean.Logs.Services.Contracts;
using Promethean.Notifications.Messages.Contracts;

namespace Promethean.CommandHandlers.Handlers
{
	public sealed class Handler : IHandler
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogService _logService;

		internal static HttpStatusCode InvalidHandlerDefaultCode = HttpStatusCode.BadRequest;

		public Handler(IServiceProvider serviceProvider, ILogService logService)
		{
			_serviceProvider = serviceProvider;
			_logService = logService;
		}

		public async Task<TCommandResult> Handle<TCommand, TCommandResult>(TCommand command)
			where TCommand : ICommand
			where TCommandResult : ICommandResult, new()
		{
			bool async = false;

			IAsyncCommandHandler<TCommand, TCommandResult> asyncHandler = null;
			ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TCommandResult>)) as ICommandHandler<TCommand, TCommandResult>;

			if (handler == null)
			{
				async = true;
				asyncHandler = _serviceProvider.GetService(typeof(IAsyncCommandHandler<TCommand, TCommandResult>)) as IAsyncCommandHandler<TCommand, TCommandResult>;
			}

			if (handler == null && asyncHandler == null)
				throw new ArgumentNullException(nameof(handler));

			TCommandResult result = new TCommandResult();

			try
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>(nameof(EOperations.Input), nameof(handler.Handle), new { Input = command }, LogLevel.Debug);

				result = async ? await asyncHandler.Handle(command) : handler.Handle(command);

				if (result == null)
				{
					result = new TCommandResult();

					IReadOnlyDictionary<string, IReadOnlyCollection<INotificationMessage>> notifications = async ? asyncHandler.Notifications : handler.Notifications;

					if (notifications.Count > 0)
						result.Populate(InvalidHandlerDefaultCode, notifications);
				}
			}
			catch (Exception exception)
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>(exception, new { Input = command }, LogLevel.Error);
			}
			finally
			{
				_logService.Log<ICommandHandler<TCommand, TCommandResult>>(nameof(EOperations.Output), nameof(handler.Handle), new { Output = result }, LogLevel.Debug);
			}

			return result;
		}
	}
}