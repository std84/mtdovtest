
using Microsoft.EntityFrameworkCore;
using missiontest.DATA.Configurations;
using missiontest.MODAL;
namespace missiontest.DATA
{
    public class dataContext: DbContext
    {
         public DbSet<User> User { get; set; }
      public DbSet<Mission> Mission { get; set; }
        public dataContext(DbContextOptions<dataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
              .ApplyConfiguration(new UserConfiguration());
            builder
              .ApplyConfiguration(new MissionConfiguration());
           

        }
    }
}