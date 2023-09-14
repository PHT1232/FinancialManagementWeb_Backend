using Microsoft.EntityFrameworkCore;
using ProjectModel.ReceiptComponents;

namespace EntityFramework
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}