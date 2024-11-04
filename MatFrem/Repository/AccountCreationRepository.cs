using MatFrem.DataContext;
using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public class AccountCreationRepository : IAccountCreationRepository
    {
        private readonly AppDBContext _context;
        public AccountCreationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<CreateAccountModel> AddProfileToDatabase(CreateAccountModel createAccountModel)
        {
            _context.Account_creation.Add(createAccountModel);
            await _context.SaveChangesAsync();
            return createAccountModel;
        }

    }
}
