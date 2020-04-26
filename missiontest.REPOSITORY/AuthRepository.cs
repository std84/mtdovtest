using System.Threading.Tasks;

using System;
using Microsoft.EntityFrameworkCore;
using missiontest.DATA;
using missiontest.MODAL;
namespace missiontest.REPOSITORY
{
    public class AuthRepository: IAuthRepository
    {
          private readonly dataContext _context;
        public AuthRepository(dataContext context){
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
           
            var user = await _context.User.Include(p => p.missionList).FirstOrDefaultAsync( x => x.Username == username);

            if (user == null) 
                return null;
            if(!VerifyPasswordHash(password,user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i<computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password, out passwordHash,out passwordSalt);
            
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt= hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _context.User.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }
    }
}