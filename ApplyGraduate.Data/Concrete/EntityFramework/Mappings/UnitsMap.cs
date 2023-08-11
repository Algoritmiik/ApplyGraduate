using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class UnitsMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Name).HasMaxLength(50);

            builder.HasOne<Faculty>(u => u.Faculty).WithMany(f => f.Units).HasForeignKey(u => u.FacultyId);

            //Shared
            builder.Property(u => u.CreatedDate).IsRequired();
            
            builder.Property(u => u.ModifiedDate).IsRequired();
            
            builder.Property(u => u.IsDeleted).IsRequired();

            builder.HasData(
                new Unit
                {
                    Id = 1,
                    Name = "Hukuk",
                    FacultyId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },
                
                new Unit
                {
                    Id = 2,
                    Name = "Bilgisayar Mühendisliği",
                    FacultyId = 2,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },
                
                new Unit
                {
                    Id = 3,
                    Name = "Makine Mühendisliği",
                    FacultyId = 2,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },
                
                new Unit
                {
                    Id = 4,
                    Name = "İnşaat Mühendisliği",
                    FacultyId = 2,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },
                
                new Unit
                {
                    Id = 5,
                    Name = "Tıp",
                    FacultyId = 3,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}