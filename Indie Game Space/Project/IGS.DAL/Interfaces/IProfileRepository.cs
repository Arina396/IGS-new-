using IGS.Domain.Entity;

namespace IGS.DAL.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<Profile> GetByName(string login);
        Task<bool> SaveProfile(Profile profile);
    }
}
