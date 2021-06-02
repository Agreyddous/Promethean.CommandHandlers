using System.ComponentModel.DataAnnotations;

namespace Promethean.CommandHandlers.Commands
{
	public interface IValidatableCommand : IValidatableObject, ICommand { }
}