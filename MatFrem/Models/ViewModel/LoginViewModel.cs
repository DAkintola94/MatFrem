using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatFrem.Models.ViewModel
{
    public class LoginViewModel
    {
		[Required(ErrorMessage = "Dette feltet må fylles ut")]
		public string Username { get; set; }
        [Required(ErrorMessage = "Dette feltet må fylles ut")]
        public string Password { get; set; }
        public string? RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

        [NotMapped]
        [ValidateNever]
        public bool PasswordFailed { get; set; } = false;
    }
}
