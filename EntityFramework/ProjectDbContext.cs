using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectModel.Chats;
using ProjectModel.ReceiptComponents;

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
        public DbSet<ReceiptItem> ReceiptItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}