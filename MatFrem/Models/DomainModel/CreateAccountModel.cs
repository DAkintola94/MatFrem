using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.DomainModel
{
    public class CreateAccountModel
    {
        [Key]
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string PhoneNr { get; set; }
        public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
