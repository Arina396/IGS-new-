using IGS.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGS.DAL.Interfaces.Jam
{
    public interface IJamTeamRepository
    {
        Task<bool> Create(JamTeam entity);
        Task<bool> Delete(JamTeam entity);
        Task<JamTeam> GetById(int id);
        Task<List<JamTeam>> Select();
    }
}