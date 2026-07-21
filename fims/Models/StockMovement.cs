using System.ComponentModel.DataAnnotations;

namespace fims.Models
{
    /// <summary>
    /// Defines the types of stock movements supported
    /// by the inventory management system.
    /// </summary>
    public enum MovementType
    {
        StockIn,
        StockOut
    }

    /// <summary>
    /// Represents a stock transaction (Stock In or Stock Out)
    /// performed within the Five's Inventory Management System (FIMS).
    /// </summary>

    // TODO: Implement the StockMovement entity.
    public class StockMovement
    {
        /// <summary>
        /// Gets the unique identifier for the stock movement.

        public int StockMovementId { get; set; }

        // TODO: Include the associated product.
        /// <summary>
        /// Gets the foreign key of the product involved
        /// in this stock transaction.

        public int ProductId { get; set; }

        // TODO: Include the quantity.
        /// <summary>
        /// Gets the quantity of items moved.

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        // TODO: Include the movement type.
        /// <summary>
        /// Gets  whether the transaction is a
        /// Stock In or Stock Out movement.
        /// </summary>
        public MovementType MovementType { get; set; }

        // TODO: Include the date/time.
        /// <summary>
        /// Gets  the date and time when the
        /// stock movement occurred.
        /// Defaults to the current system time.
        /// </summary>
        public DateTime MovementDate { get; set; } = DateTime.Now;

        // TODO: Include the user who performed the action.
        /// <summary>
        /// Gets or sets the Id of the authenticated user
        /// who recorded the stock movement.
        /// </summary>
        public string ApplicationUserId { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property.
        /// Gets or sets the product involved in this stock movement.
        /// </summary>
        public Product Product { get; set; } = null!;

        
        /// Navigation property.
        /// Gets the user who recorded this stock movement.
        
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}