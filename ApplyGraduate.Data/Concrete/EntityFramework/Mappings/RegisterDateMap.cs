using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class DatesMap : IEntityTypeConfiguration<Dates>
    {
        public void Configure(EntityTypeBuilder<Dates> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.RegisterBeginningDate).IsRequired();
            
            builder.Property(d => d.RegisterEndingDate).IsRequired();
            
            builder.Property(d => d.GraduationDate).IsRequired();

            //Shared
            builder.Property(d => d.CreatedDate).IsRequired();
            
            builder.Property(d => d.ModifiedDate).IsRequired();
            
            builder.Property(d => d.IsDeleted).IsRequired();

            builder.HasData(
                new Dates
                {
                    Id = 1,
                    RegisterBeginningDate = DateTime.Now,
                    RegisterEndingDate = DateTime.Now,
                    GraduationDate = DateTime.Now.AddDays(10),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}