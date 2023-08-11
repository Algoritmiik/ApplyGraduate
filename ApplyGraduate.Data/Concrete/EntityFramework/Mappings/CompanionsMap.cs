using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class CompanionsMap : IEntityTypeConfiguration<Companion>
    {
        public void Configure(EntityTypeBuilder<Companion> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(50);

            builder.Property(c => c.Surname).IsRequired();
            builder.Property(c => c.Surname).HasMaxLength(50);

            builder.Property(c => c.HesCode).IsRequired();
            builder.Property(c => c.HesCode).HasMaxLength(10);
            
            builder.Property(c => c.PhoneNumber).IsRequired();
            builder.Property(c => c.PhoneNumber).HasMaxLength(10);

            builder.HasOne<Student>(c => c.Student).WithMany(s => s.Companions).HasForeignKey(c => c.StudentId);

            builder.Property(c => c.DidJoin).IsRequired();

            //Shared
            builder.Property(c => c.CreatedDate).IsRequired();
            
            builder.Property(c => c.ModifiedDate).IsRequired();
            
            builder.Property(c => c.IsDeleted).IsRequired();

            builder.HasData(
                new Companion
                {
                    Id = 1,
                    Name = "Samet",
                    Surname = "Aslan",
                    HesCode = "A1B2C3D4F5",
                    PhoneNumber = "5424173626",
                    StudentId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DidJoin = false,
                    IsDeleted = false,
                }
            );
        }
    }
}