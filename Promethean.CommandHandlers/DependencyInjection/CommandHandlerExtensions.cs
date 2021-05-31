using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Promethean.CommandHandlers.Handlers;
using System.Reflection;

namespace Promethean.CommandHandlers.DependencyInjection
{
	public static class CommandHandlerExtensions
	{
		public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IHandler, Handler>();

			foreach (Type existingType in Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load).SelectMany(assembly => assembly.DefinedTypes))
				foreach (Type implementedCommandHandler in existingType.GetInterfaces().Where(implementedInterface => implementedInterface.IsGenericType && (implementedInterface.GetGenericTypeDefinition().Equals(typeof(ICommandHandler<,>)) || implementedInterface.GetGenericTypeDefinition().Equals(typeof(IAsyncCommandHandler<,>)))))
					serviceCollection.AddScoped(implementedCommandHandler, existingType);

			return serviceCollection;
		}
	}
}