using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorSample.Data
{
	public class Todo : IValidatableObject
    {
        public String Name { get; set; }

        public String Title { get; set; }

        public String Body { get; set; }

        public Boolean Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Active)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    yield return new ValidationResult($"The Name field is required", new[] { "Name" });
                }
                if (string.IsNullOrEmpty(Title))
                {
                    yield return new ValidationResult($"The Title field is required", new[] { "Title" });
                }
            }
        }
    }
}

