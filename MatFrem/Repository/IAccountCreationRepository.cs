using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IAccountCreationRepository
    {
        Task<CreateAccountModel> AddProfileToDatabase(CreateAccountModel createAccountModel);

    }
}
