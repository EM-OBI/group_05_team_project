using System.ComponentModel.DataAnnotations;

namespace fims.Models
{
    /// <summary>
    /// Represents an inventory item managed by the Five's Inventory
    /// Management System (FIMS).
    /// </summary>

    // TODO: Implement the Product entity.
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        // TODO: Include properties such as name.
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets an optional description of the product.
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        // TODO: Include the category assigned to the product.
        /// <summary>
        /// Gets or sets the foreign key of the category
        /// this product belongs to.
        /// </summary>
        public int CategoryId { get; set; }

        // TODO: Include the supplier assigned to the product.
        /// <summary>
        /// Gets or sets the foreign key of the supplier
        /// providing this product.
        /// </summary>
        public int SupplierId { get; set; }

        // TODO: Include the product price.
        /// <summary>
        /// Gets or sets the selling price of the product.
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        // TODO: Include the current stock quantity.
        /// <summary>
        /// Gets or sets the current quantity available in inventory.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int CurrentStockQuantity { get; set; }

        // TODO: Include the minimum stock threshold.
        /// <summary>
        /// Gets or sets the minimum quantity allowed before
        /// the product is considered low in stock.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int MinimumStockThreshold { get; set; }

        /// <summary>
        /// Navigation property.
        /// Gets or sets the category this product belongs to.
        /// </summary>
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Navigation property.
        /// Gets or sets the supplier of this product.
        /// </summary>
        public Supplier Supplier { get; set; } = null!;

        /// <summary>
        /// Navigation property.
        /// One product can have many stock movements.
        /// </summary>
        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}