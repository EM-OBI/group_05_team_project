using System.ComponentModel.DataAnnotations;

namespace fims.Models
{
    /// <summary>
    /// Represents a supplier that provides inventory items
    /// for the Five's Inventory Management System (FIMS).
    /// </summary>

    // TODO: Implement the Supplier entity.
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the unique identifier for the supplier.
        /// </summary>
        public int SupplierId { get; set; }

        // TODO: Include supplier details (e.g., name).
        /// <summary>
        /// Gets or sets the supplier's name.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // TODO: Include supplier contact information.
        /// <summary>
        /// Gets or sets the supplier's contact information,
        /// such as a phone number, email address, or office location.
        /// </summary>
        [Required]
        [StringLength(300)]
        public string ContactInfo { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property.
        /// One supplier can provide many products.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}