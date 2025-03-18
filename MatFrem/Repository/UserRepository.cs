using MatFrem.DataContext;
using MatFrem.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace MatFrem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        public UserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }


        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await _authDbContext.Users.ToListAsync();

            var systemAdminUser = await _authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "sysadmin@test.com"); //variable to check if sysadmin exists

            if(systemAdminUser!= null) //if there is a systemadmin in the list
            {
                users.Remove(systemAdminUser); //remove the systemadmin from the list of users
            }

            return users;
        }

        public async Task<ApplicationUser> DeleteUserById(string id)
        {
            var users = await _authDbContext.Users.ToListAsync();

            var matchEmail = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == id);
            if(matchEmail!= null)
            {
                return null;
            }

            return null;
        }




    }
}
