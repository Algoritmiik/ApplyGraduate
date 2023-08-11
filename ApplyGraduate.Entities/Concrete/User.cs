using Microsoft.AspNetCore.Identity;

namespace ApplyGraduate.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}