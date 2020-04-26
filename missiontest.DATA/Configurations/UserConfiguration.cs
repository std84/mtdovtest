using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using missiontest.MODAL;
namespace missiontest.DATA.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
       

        public void Configure(EntityTypeBuilder<User> builder)
        {
             builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id);
            builder
                .Property(m => m.Username)
                .IsRequired();
            builder
                .Property(m => m.PasswordHash)
                .IsRequired();
            builder
                .Property(m => m.PasswordSalt)
                .IsRequired();
            builder
                .Property(m => m.firstName)
                .IsRequired();
            builder
                .Property(m => m.lastName)
                .IsRequired();
            builder
                .Property(m => m.address);
            builder
                .Property(m => m.city);
            builder
                .Property(m => m.Age)
                .IsRequired();
            builder
                .Property(m => m.phone)
                .IsRequired();
            builder
                .Property(m => m.email)
                .IsRequired();
            builder
                .Property(m => m.sex)
                .IsRequired();
            builder
                .Property(m => m.token);
            builder
             .ToTable("User");
        }
    }
}