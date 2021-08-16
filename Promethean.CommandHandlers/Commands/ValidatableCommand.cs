using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Promethean.CommandHandlers.Commands.Contracts;
using Promethean.Notifications;
using Promethean.Notifications.Extensions;
using Promethean.Notifications.Validators.Contracts;

namespace Promethean.CommandHandlers.Commands
{
	public abstract class ValidatableCommand : Notifiable, IValidatable, IValidatableCommand
	{
		public abstract void Validate();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			Validate();

			return Notifications.AsValidationResult();
		}
	}
}