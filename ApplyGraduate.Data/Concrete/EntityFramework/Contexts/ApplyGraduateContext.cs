using ApplyGraduate.Data.Concrete.EntityFramework.Mappings;
using ApplyGraduate.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Contexts
{
    public class ApplyGraduateContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Companion> Companions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Program> Program { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentsMap());
            modelBuilder.ApplyConfiguration(new CompanionsMap());
            modelBuilder.ApplyConfiguration(new FacultiesMap());
            modelBuilder.ApplyConfiguration(new UnitsMap());
            modelBuilder.ApplyConfiguration(new ProgramsMap());
            modelBuilder.ApplyConfiguration(new GownSizeMap());
            modelBuilder.ApplyConfiguration(new DatesMap());
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
        }
    }


    
}