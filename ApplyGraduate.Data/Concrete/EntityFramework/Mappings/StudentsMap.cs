using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Mappings
{
    public class StudentsMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(50);
            
            builder.Property(s => s.Surname).IsRequired();
            builder.Property(s => s.Surname).HasMaxLength(50);

            builder.HasOne<Program>(s => s.Program).WithMany(p => p.Students).HasForeignKey(s => s.ProgramId);

            builder.Property(s => s.GownRequest).IsRequired();

            builder.Property(s => s.GownSize).IsRequired(false);

            builder.Property(s => s.DidPaid).IsRequired();

            builder.Property(s => s.DidTake).IsRequired();

            builder.Property(s => s.CompanionStatus).IsRequired();

            builder.Property(s => s.DidJoin).IsRequired();
            
            builder.Property(s => s.HaveApplied).IsRequired();

            builder.Property(s => s.Note).IsRequired(false);
            builder.Property(s => s.Note).HasMaxLength(300);

            //Shared
            builder.Property(s => s.CreatedDate).IsRequired();
            
            builder.Property(s => s.ModifiedDate).IsRequired();
            
            builder.Property(s => s.IsDeleted).IsRequired();

            builder.HasData(
                new Student
                {
                    Id = 1,
                    Name = "Mert",
                    Surname = "Aslan",
                    ProgramId = 2,
                    GownRequest = false,
                    GownSize = null,
                    CompanionStatus = false,
                    DidJoin = false,
                    HaveApplied = false,
                    Note = null,
                    DidPaid = false,
                    DidTake = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                },

                new Student
                {
                    Id = 2,
                    Name = "Recep",
                    Surname = "Yayla",
                    ProgramId = 1,
                    GownRequest = false,
                    GownSize = null,
                    CompanionStatus = false,
                    DidJoin = false,
                    HaveApplied = false,
                    Note = null,
                    DidPaid = false,
                    DidTake = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                },

                new Student
                {
                    Id = 3,
                    Name = "Cihan",
                    Surname = "Eyuboglu",
                    ProgramId = 1,
                    GownRequest = false,
                    GownSize = null,
                    CompanionStatus = false,
                    DidJoin = false,
                    HaveApplied = false,
                    Note = null,
                    DidPaid = false,
                    DidTake = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                },

                new Student
                {
                    Id = 4,
                    Name = "Berk",
                    Surname = "Kayatepe",
                    ProgramId = 3,
                    GownRequest = false,
                    GownSize = null,
                    CompanionStatus = false,
                    DidJoin = false,
                    HaveApplied = false,
                    Note = null,
                    DidPaid = false,
                    DidTake = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                },

                new Student
                {
                    Id = 5,
                    Name = "Muhammed",
                    Surname = "Cınıslı",
                    ProgramId = 5,
                    GownRequest = false,
                    GownSize = null,
                    CompanionStatus = false,
                    DidJoin = false,
                    HaveApplied = false,
                    Note = null,
                    DidPaid = false,
                    DidTake = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false,
                }
            );
        }
    }
}