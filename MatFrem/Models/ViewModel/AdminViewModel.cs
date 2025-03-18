using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
    public class AdminViewModel
    {
        public List<CreateProfileViewModel>? ProfileCreation { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPhoneNr { get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]

        public string Password { get; set; }

        public bool? IsDriver { get; set; }
        public bool IsDriverChecked => IsDriver ?? false;
        public bool? IsCustomer { get; set; }
        public bool IsCustomerChecked => IsCustomer ?? false;
    }
}
