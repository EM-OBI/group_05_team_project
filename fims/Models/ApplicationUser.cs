using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace fims.Models
{
    /// <summary>
    /// Represents an authenticated user of the Five's Inventory Management System (FIMS),
    /// such as an Admin or Employee.
    /// This class extends ASP.NET Core IdentityUser to provide built-in
    /// authentication and authorization functionality while allowing
    /// additional application-specific user information.
    /// </summary>

    // TODO: Implement the ApplicationUser entity.
    public class ApplicationUser : IdentityUser
    {
        // TODO: Extend ASP.NET Core IdentityUser if additional user
        // information or role-specific properties are needed.

        /// <summary>
        /// Gets or sets the user's full name.
        /// This property is used for displaying the user's name
        /// throughout the application.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property.
        /// One user can record many stock movements.
        /// </summary>
        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}