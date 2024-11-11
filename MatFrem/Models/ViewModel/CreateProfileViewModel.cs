using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
    public class CreateProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set;  }

        [MinLength(8)]
        public string PhoneNr { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passordet stemmer ikke.")]
        public string ConfirmPassword { get; set; }


    }
}
