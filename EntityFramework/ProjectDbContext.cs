using EntityFramework.DbEntities.Chats;
using EntityFramework.DbEntities.Groups;
using EntityFramework.DbEntities.ReceiptComponents;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class ProjectDbContext : IdentityDbContext<IdentityUser>
    {
        public ProjectDbContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public DbSet<GroupRoles> GroupRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}