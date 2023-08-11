using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class GownSizeMap : IEntityTypeConfiguration<GownSize>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GownSize> builder)
        {
            builder.HasKey(gs => gs.Id);
            builder.Property(gs => gs.Id).ValueGeneratedOnAdd();

            builder.Property(gs => gs.Size).IsRequired();
            builder.Property(gs => gs.Size).HasMaxLength(5);

            //Shared
            builder.Property(gs => gs.CreatedDate).IsRequired();
            
            builder.Property(gs => gs.ModifiedDate).IsRequired();
            
            builder.Property(gs => gs.IsDeleted).IsRequired();

            builder.HasData(
                new GownSize
                {
                    Id = 1,
                    Size = "XS",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new GownSize
                {
                    Id = 2,
                    Size = "S",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new GownSize
                {
                    Id = 3,
                    Size = "M",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new GownSize
                {
                    Id = 4,
                    Size = "L",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new GownSize
                {
                    Id = 5,
                    Size = "XL",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                },

                new GownSize
                {
                    Id = 6,
                    Size = "XXL",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}