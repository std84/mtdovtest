using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using missiontest.MODAL;

namespace missiontest.DATA.Configurations
{
    public class MissionConfiguration: IEntityTypeConfiguration<Mission>
    {
       

   

        public void Configure(EntityTypeBuilder<Mission> builder)
        {
               builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id);
            builder
                .Property(m => m.userId)
                .IsRequired();
            builder
                .Property(m => m.name)
                .IsRequired();
            builder
                .Property(m => m.description)
                .IsRequired();
                
            builder
                .Property(m => m.missionDate)
                .IsRequired();
            builder
                .Property(m => m.priority)
                .IsRequired();
            builder
                .Property(m => m.isActive)
                .IsRequired();
            builder
                .Property(m => m.isDone)
                .IsRequired();
           builder
             .ToTable("Mission");
        }
    }
}