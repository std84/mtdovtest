using System.Collections.Generic;
using System.Threading.Tasks;
using missiontest.MODAL;

namespace missiontest.REPOSITORY
{
    public interface ImissionRepository
    {
        
         
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
        Task<IList<Mission>>  GetMissions(int id);
        Task<Mission>  GetMission(int id);
        Task<bool> ShareMission(int id);
    }
}