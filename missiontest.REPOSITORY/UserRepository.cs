using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using missiontest.DATA;
using missiontest.MODAL;

namespace missiontest.REPOSITORY
{
    public class UserRepository:IUserRepository
    {
        
        private readonly dataContext _context;
        public UserRepository(dataContext context){
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }


        public async Task<User> GetUserByMail(string email)
        {
                var user = await _context.User.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }

        public async Task<bool> SaveAll()
        {
              return await _context.SaveChangesAsync() > 0;
        }
 
    }
}