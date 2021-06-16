using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Promethean.CommandHandlers.Handlers;
using System.Reflection;
using Promethean.CommandHandlers.Handlers.Contracts;

namespace Promethean.CommandHandlers.DependencyInjection
{
	public static class CommandHandlerExtensions
	{
		public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IHandler, Handler>();

			foreach (Type existingType in AppDomain.CurrentDomain.GetAssemblies().Union(Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load)).SelectMany(assembly => assembly.GetTypes()))
				foreach (Type implementedCommandHandler in existingType.GetInterfaces().Where(implementedInterface => implementedInterface.IsGenericType && (implementedInterface.GetGenericTypeDefinition().Equals(typeof(ICommandHandler<,>)) || implementedInterface.GetGenericTypeDefinition().Equals(typeof(IAsyncCommandHandler<,>)))))
					serviceCollection.AddScoped(implementedCommandHandler, existingType);

			return serviceCollection;
		}
	}
}