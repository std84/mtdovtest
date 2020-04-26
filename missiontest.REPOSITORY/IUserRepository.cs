using System.Threading.Tasks;
using missiontest.MODAL;

namespace missiontest.REPOSITORY
{
    public interface IUserRepository
    {
        
        void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
        // Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<User> GetUserByMail(string email);
    }
}