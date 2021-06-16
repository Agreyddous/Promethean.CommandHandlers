using System.ComponentModel.DataAnnotations;

namespace Promethean.CommandHandlers.Commands.Contracts
{
	public interface IValidatableCommand : IValidatableObject, ICommand { }
}