using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class ProgramsMap : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);

            builder.HasOne<Unit>(p => p.Unit).WithMany(u => u.Programs).HasForeignKey(p => p.UnitId);

            //Shared
            builder.Property(p => p.CreatedDate).IsRequired();
            
            builder.Property(p => p.ModifiedDate).IsRequired();
            
            builder.Property(p => p.IsDeleted).IsRequired();

            builder.HasData(
                new Program
                {
                    Id = 1,
                    Name = "Hukuk",
                    UnitId = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Program
                {
                    Id = 2,
                    Name = "Bilgisayar Mühendisliği",
                    UnitId = 2,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Program
                {
                    Id = 3,
                    Name = "Makine Mühendisliği",
                    UnitId = 3,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Program
                {
                    Id = 4,
                    Name = "İnşaat Mühendisliği",
                    UnitId = 4,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Program
                {
                    Id = 5,
                    Name = "Tıp",
                    UnitId = 5,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}