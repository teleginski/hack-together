using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorSample.Data
{
	public class Calendar : IValidatableObject
    {
		public String Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartWith { get; set; }

        [Required]
        public DateTime EndWith { get; set; }

        public String Body { get; set; }

        [EmailAddress]
        public String? Attendee { get; set; }

        public Boolean Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Active)
            {
                if (string.IsNullOrEmpty(Subject))
                {
                    yield return new ValidationResult($"The Subject field is required", new[] { "Subject" });
                }
                if (EndWith < StartWith)
                {
                    yield return new ValidationResult($"The End Date cannot be less than Start Date", new[] { "EndWith" });
                }

                if (StartWith > EndWith)
                {
                    yield return new ValidationResult($"The Start Date cannot be greater than End Date", new[] { "StartWith" });
                }
            }
        }
    }
}

