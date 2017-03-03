using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	internal abstract class BuilderBase
	{
		protected abstract void ValidateInternal(Action<string> actionOnInvalid);

		public void Validate()
		{
			ValidateInternal(errorMessage => { throw new ValidationException(errorMessage); });
		}

		public bool TryValidate(ICollection<ValidationResult> validationResults)
		{
			if (validationResults == null) throw new ArgumentNullException(nameof(validationResults));
			ValidateInternal(errorMessage => { validationResults.Add(new ValidationResult(errorMessage)); });
			return validationResults.Any();
		}
	}
}