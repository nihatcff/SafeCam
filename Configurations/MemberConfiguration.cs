using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeCam.Models;

namespace SafeCam.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Designation).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.ImagePath).IsRequired();
        }
    }
}
