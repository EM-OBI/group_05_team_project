using System.ComponentModel.DataAnnotations;

namespace fims.Models
{
    /// <summary>
    /// Represents a product category in the Five's Inventory Management System (FIMS).
    /// Categories help organize products into logical groups
    /// such as Beverages, Electronics, or Groceries.
    /// </summary>

    // TODO: Implement the Category entity.
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }

        // TODO: Include properties for the category name.
        /// <summary>
        /// Gets or sets the name of the category.
        /// This field is required and is limited to 100 characters.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // TODO: Include any relationships to the products
        // that belong to this category.

        /// <summary>
        /// Navigation property.
        /// One category can contain many products.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}