using fims.Models;

namespace fims.Interfaces
{
    public interface IStockMovementService
    {
        Task<List<StockMovement>> GetAllAsync();
        Task<List<StockMovement>> GetByProductAsync(int productId);
        Task<List<StockMovement>> GetByTypeAsync(MovementType type);
        Task<StockMovement?> GetByIdAsync(int id);
        Task<StockMovement> CreateAsync(StockMovement movement);
        Task<int> GetTotalStockInAsync();
        Task<int> GetTotalStockOutAsync();
    }
}
