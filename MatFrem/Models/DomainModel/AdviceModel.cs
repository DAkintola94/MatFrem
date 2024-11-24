using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
	public class AdviceModel
	{
		public int PostId { get; set; }
		[Required]
		public string Author { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(200)]
		public string AdviceMessage { get; set; }

		[Required, EmailAddress]

		public string Email { get; set; }

		public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

	}
}
