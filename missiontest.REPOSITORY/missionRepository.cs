using System.Threading.Tasks;
using missiontest.DATA;
using missiontest.MODAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace missiontest.REPOSITORY
{
    public class missionRepository : ImissionRepository
    {
           private readonly dataContext _context;
        public missionRepository(dataContext context){
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

        public async Task<IList<Mission>> GetMissions(int id)
        {
           // 
               return await  _context.Mission.Where(u =>u.userId == id ).ToListAsync();
        
        }
      public async Task<Mission> GetMission(int id)
        {
           // 
               return await  _context.Mission.Where(u =>u.Id == id ).FirstOrDefaultAsync();
        
        }
        public async Task<bool> SaveAll()
        {
             return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ShareMission(int id)
        {
            var missions = await  _context.Mission.Where(u =>u.userId == id ).ToListAsync();

            return true;
        }
    }
}