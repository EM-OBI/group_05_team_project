using Microsoft.EntityFrameworkCore;
using fims.Models;

namespace fims.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (!await db.Categories.AnyAsync())
            {
                db.Categories.AddRange(
                    new Category { Name = "Bakery" },
                    new Category { Name = "Dairy" },
                    new Category { Name = "Grains" },
                    new Category { Name = "Beverages" },
                    new Category { Name = "Produce" }
                );
            }

            if (!await db.Suppliers.AnyAsync())
            {
                db.Suppliers.AddRange(
                    new Supplier { Name = "Acme Foods", ContactInfo = "555-2211 / sales@acme.com" },
                    new Supplier { Name = "Fresh Market", ContactInfo = "555-3344 / info@fresh.com" },
                    new Supplier { Name = "Food Company", ContactInfo = "555-7788 / contact@food.com" }
                );
            }

            await db.SaveChangesAsync();

            if (!await db.Products.AnyAsync())
            {
                var categories = await db.Categories.ToDictionaryAsync(c => c.Name);
                var suppliers = await db.Suppliers.ToDictionaryAsync(s => s.Name);

                db.Products.AddRange(
                    new Product { Name = "Bread", CategoryId = categories["Bakery"].CategoryId, SupplierId = suppliers["Acme Foods"].SupplierId, Price = 2.50m, CurrentStockQuantity = 60, MinimumStockThreshold = 20 },
                    new Product { Name = "Croissant", CategoryId = categories["Bakery"].CategoryId, SupplierId = suppliers["Acme Foods"].SupplierId, Price = 1.75m, CurrentStockQuantity = 40, MinimumStockThreshold = 15 },
                    new Product { Name = "Milk", CategoryId = categories["Dairy"].CategoryId, SupplierId = suppliers["Fresh Market"].SupplierId, Price = 3.20m, CurrentStockQuantity = 80, MinimumStockThreshold = 25 },
                    new Product { Name = "Cheddar Cheese", CategoryId = categories["Dairy"].CategoryId, SupplierId = suppliers["Fresh Market"].SupplierId, Price = 5.50m, CurrentStockQuantity = 35, MinimumStockThreshold = 12 },
                    new Product { Name = "White Rice", CategoryId = categories["Grains"].CategoryId, SupplierId = suppliers["Food Company"].SupplierId, Price = 4.00m, CurrentStockQuantity = 90, MinimumStockThreshold = 30 },
                    new Product { Name = "Oats", CategoryId = categories["Grains"].CategoryId, SupplierId = suppliers["Food Company"].SupplierId, Price = 3.75m, CurrentStockQuantity = 50, MinimumStockThreshold = 20 },
                    new Product { Name = "Orange Juice", CategoryId = categories["Beverages"].CategoryId, SupplierId = suppliers["Fresh Market"].SupplierId, Price = 4.25m, CurrentStockQuantity = 45, MinimumStockThreshold = 15 },
                    new Product { Name = "Coffee Beans", CategoryId = categories["Beverages"].CategoryId, SupplierId = suppliers["Food Company"].SupplierId, Price = 12.00m, CurrentStockQuantity = 25, MinimumStockThreshold = 8 },
                    new Product { Name = "Tomatoes", CategoryId = categories["Produce"].CategoryId, SupplierId = suppliers["Fresh Market"].SupplierId, Price = 1.90m, CurrentStockQuantity = 100, MinimumStockThreshold = 30 },
                    new Product { Name = "Apples", CategoryId = categories["Produce"].CategoryId, SupplierId = suppliers["Acme Foods"].SupplierId, Price = 2.20m, CurrentStockQuantity = 70, MinimumStockThreshold = 20 }
                );

                await db.SaveChangesAsync();
            }
        }
    }
}
