using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
	public class AdviceViewModel
	{
		public int PostId { get; set; }
		[Required]
		public string Author { get; set; }

		[Required]
		public string AdviceMessage{get; set; }

		[Required, EmailAddress]
		
		public string Email { get; set; }

		public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

	}
}
