using Microsoft.EntityFrameworkCore;
using fims.Data;
using fims.Interfaces;
using fims.Models;

namespace fims.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _db;

        public DashboardService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _db.Products.CountAsync();
        }

        public async Task<decimal> GetTotalStockValueAsync()
        {
            return await _db.Products.SumAsync(p => p.Price * p.CurrentStockQuantity);
        }

        public async Task<int> GetLowStockCountAsync()
        {
            return await _db.Products.CountAsync(p => p.CurrentStockQuantity <= p.MinimumStockThreshold);
        }

        public async Task<List<DashboardLowStockItem>> GetLowStockItemsAsync()
        {
            return await _db.Products
                .Where(p => p.CurrentStockQuantity <= p.MinimumStockThreshold)
                .OrderBy(p => p.CurrentStockQuantity)
                .Select(p => new DashboardLowStockItem
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    CurrentStock = p.CurrentStockQuantity,
                    MinimumStock = p.MinimumStockThreshold
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalCategoriesAsync()
        {
            return await _db.Categories.CountAsync();
        }

        public async Task<int> GetTotalSuppliersAsync()
        {
            return await _db.Suppliers.CountAsync();
        }

        public async Task<int> GetTotalStockInAsync()
        {
            return await _db.StockMovements
                .Where(sm => sm.MovementType == MovementType.StockIn)
                .SumAsync(sm => sm.Quantity);
        }

        public async Task<int> GetTotalStockOutAsync()
        {
            return await _db.StockMovements
                .Where(sm => sm.MovementType == MovementType.StockOut)
                .SumAsync(sm => sm.Quantity);
        }
    }
}
