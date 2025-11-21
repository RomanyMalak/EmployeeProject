using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestProject.Model;

namespace TestProject.Context
{
    public class App_context:IdentityDbContext<NewUser>
    {

        public App_context(DbContextOptions<App_context>options):base(options) 
        {
            
        }
        public DbSet<Employee>Employees { get; set; }
    }
}
