using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Promethean.Notifications;
using Promethean.Notifications.Validators;

namespace Promethean.CommandHandlers.Commands
{
	public abstract class ValidatableCommand : Notifiable, IValidatable, IValidatableCommand
	{
		public abstract void Validate();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			Validate();

			return Notifications.Select(notification => new ValidationResult(notification.Message, new[] { notification.Property }));
		}
	}
}