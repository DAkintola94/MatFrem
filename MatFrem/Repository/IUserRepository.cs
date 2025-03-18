using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> DeleteUserById(string id);


    }
}
