using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorSample.Data
{
	public class Email : IValidatableObject
    {
        [Required]
        public String Type { get; set; }

        [EmailAddress]
        public String From { get; set; }
        public String Subject { get; set; }

        public String Attachment { get; set; }

        public Boolean Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Active)
            {
                if (string.IsNullOrEmpty(Attachment))
                {
                    yield return new ValidationResult($"The Attachment field is required", new[] { "Attachment" });
                }
            }

            if (Type.Equals("From") && string.IsNullOrEmpty(From))
            {
                yield return new ValidationResult($"The From field is required", new[] { "From" });
            }

            if (Type.Equals("Subject") && string.IsNullOrEmpty(Subject))
            {
                yield return new ValidationResult($"The Subject field is required", new[] { "Subject" });
            }
        }
    }
}

