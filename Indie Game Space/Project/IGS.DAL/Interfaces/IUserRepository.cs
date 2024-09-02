using IGS.Domain.Entity;

namespace IGS.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByLogin(string login);

        Task<User> GetByEmail(string email);
    }
}
