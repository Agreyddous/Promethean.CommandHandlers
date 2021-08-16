using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Promethean.CommandHandlers.Handlers;
using System.Reflection;
using Promethean.CommandHandlers.Handlers.Contracts;
using System.Net;

namespace Promethean.CommandHandlers.DependencyInjection
{
	public static class CommandHandlerExtensions
	{
		public static IServiceCollection AddCommandHandlers(this IServiceCollection serviceCollection, HttpStatusCode invalidHandlerDefaultCode = HttpStatusCode.BadRequest) => AddCommandHandlers<Handler>(serviceCollection, invalidHandlerDefaultCode);
		public static IServiceCollection AddCommandHandlers<THandler>(this IServiceCollection serviceCollection, HttpStatusCode invalidHandlerDefaultCode = HttpStatusCode.BadRequest) where THandler : class, IHandler
		{
			Handler.InvalidHandlerDefaultCode = invalidHandlerDefaultCode;
			serviceCollection.AddScoped<IHandler, THandler>();

			foreach (Type existingType in AppDomain.CurrentDomain.GetAssemblies().Union(Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load)).SelectMany(assembly => assembly.GetTypes()))
				if (existingType.IsClass && !existingType.IsAbstract)
					foreach (Type implementedCommandHandler in existingType.GetInterfaces().Where(implementedInterface => implementedInterface.IsGenericType && (implementedInterface.GetGenericTypeDefinition().Equals(typeof(ICommandHandler<,>)) || implementedInterface.GetGenericTypeDefinition().Equals(typeof(IAsyncCommandHandler<,>)))))
						serviceCollection.AddScoped(implementedCommandHandler, existingType);

			return serviceCollection;
		}
	}
}