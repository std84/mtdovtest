using System.Threading.Tasks;
using missiontest.MODAL;

namespace missiontest.REPOSITORY
{
    
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExist(string username);
    }
}