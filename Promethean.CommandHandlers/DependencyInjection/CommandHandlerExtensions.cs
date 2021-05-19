using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Promethean.CommandHandlers.Handlers;

namespace Promethean.CommandHandlers.DependencyInjection
{
	public static class CommandHandlerExtensions
	{
		public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IHandler, Handler>();

			foreach (Type commandHandlerType in Assembly.GetExecutingAssembly().GetTypes())
				foreach (Type implementedCommandHandler in commandHandlerType.GetInterfaces().Where(implementedInterface => implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition().Equals(typeof(ICommandHandler<,>))))
					serviceCollection.AddScoped(implementedCommandHandler, commandHandlerType);

			return serviceCollection;
		}
	}
}