using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorSample.Data
{
	public class Automation
	{
		public Automation()
		{
			Id = Guid.NewGuid().ToString();
			Email = new Email();
			Todo = new Todo();
			Calendar = new Calendar();
		}

		public String Id { get; set; }

        [Required]
        public String Name { get; set; }

        [ValidateComplexType]
        public Email Email { get; set; }

        [ValidateComplexType]
        public Todo Todo { get; set; }

        [ValidateComplexType]
        public Calendar Calendar { get; set; }
    }
}
