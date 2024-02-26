using EntityFramework;
using EntityFramework.DbEntities.ReceiptComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repository
{
    public class ReceiptRepository : IDataRepository<Receipt>
    {
        readonly ProjectDbContext _dbContext;

        public ReceiptRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Receipt entity)
        {
            await _dbContext.Receipts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(Receipt entity)
        {
            _dbContext.Receipts.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<Receipt> Get(long id)
        {
            Receipt receipt = await _dbContext.Receipts.FirstOrDefaultAsync(e => e.Id == id);
            return receipt;
        }

        public async Task<IEnumerable<Receipt>> GetAll()
        {
            List<Receipt> receipts = await _dbContext.Receipts.ToListAsync();
            return receipts;
        }

        public async Task Update(Receipt entity, long id)
        {
            Receipt receipt = await _dbContext.Receipts.FirstOrDefaultAsync(e => e.Id == id);
            receipt.StoreName = entity.StoreName;
            receipt.Address = entity.Address;
            receipt.TotalQuantity = entity.TotalQuantity;
            receipt.Total = entity.Total;
            receipt.Items = entity.Items;

            await _dbContext.SaveChangesAsync();
        }
    }
}
