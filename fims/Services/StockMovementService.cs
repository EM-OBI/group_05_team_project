using Microsoft.EntityFrameworkCore;
using fims.Data;
using fims.Interfaces;
using fims.Models;

namespace fims.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly AppDbContext _db;

        public StockMovementService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<StockMovement>> GetAllAsync()
        {
            return await _db.StockMovements
                .Include(sm => sm.Product)
                .OrderByDescending(sm => sm.MovementDate)
                .ToListAsync();
        }

        public async Task<List<StockMovement>> GetByProductAsync(int productId)
        {
            return await _db.StockMovements
                .Include(sm => sm.Product)
                .Where(sm => sm.ProductId == productId)
                .OrderByDescending(sm => sm.MovementDate)
                .ToListAsync();
        }

        public async Task<List<StockMovement>> GetByTypeAsync(MovementType type)
        {
            return await _db.StockMovements
                .Include(sm => sm.Product)
                .Where(sm => sm.MovementType == type)
                .OrderByDescending(sm => sm.MovementDate)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await _db.StockMovements
                .Include(sm => sm.Product)
                .FirstOrDefaultAsync(sm => sm.StockMovementId == id);
        }

        public async Task<StockMovement> CreateAsync(StockMovement movement)
        {
            _db.StockMovements.Add(movement);
            await _db.SaveChangesAsync();
            return movement;
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
