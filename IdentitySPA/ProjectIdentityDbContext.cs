using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentitySPA
{
    public class ProjectIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public ProjectIdentityDbContext(DbContextOptions<ProjectIdentityDbContext> options) : base(options) { }
    }
}