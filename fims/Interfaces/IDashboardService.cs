namespace fims.Interfaces
{
    public interface IDashboardService
    {
        Task<int> GetTotalProductsAsync();
        Task<decimal> GetTotalStockValueAsync();
        Task<int> GetLowStockCountAsync();
        Task<List<DashboardLowStockItem>> GetLowStockItemsAsync();
        Task<int> GetTotalCategoriesAsync();
        Task<int> GetTotalSuppliersAsync();
        Task<int> GetTotalStockInAsync();
        Task<int> GetTotalStockOutAsync();
    }

    public class DashboardLowStockItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public int ProductId { get; set; }
    }
}
