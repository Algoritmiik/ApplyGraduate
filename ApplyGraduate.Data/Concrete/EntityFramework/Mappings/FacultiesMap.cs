using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class FacultiesMap : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Name).HasMaxLength(50);

            //Shared
            builder.Property(f => f.CreatedDate).IsRequired();
            
            builder.Property(f => f.ModifiedDate).IsRequired();
            
            builder.Property(f => f.IsDeleted).IsRequired();

            builder.HasData(
                new Faculty
                {
                    Id = 1,
                    Name = "Hukuk",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Faculty
                {
                    Id = 2,
                    Name = "Mühendislik",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new Faculty
                {
                    Id = 3,
                    Name = "Tıp Fakültesi",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}