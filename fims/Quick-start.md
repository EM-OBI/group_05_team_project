# FIMS Quick Start

FIMS (Five's Inventory Management System) tracks products, stock movements, suppliers, and categories.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Run the app

From the project root, run:

```bash
dotnet restore
dotnet run
```

Open the URL printed in the terminal (normally `https://localhost:7193` or `http://localhost:5235`). On first launch, FIMS creates or updates `fims.db` and adds example suppliers and categories.

## Sign in or create an account

Sign in from **Login**, or select **Create an account** to register a standard user. The default administrator account is:

```text
Email:    admin@fims.local
Password: Admin@12345
```

Change these bootstrap credentials before deployment with `AdminBootstrap:Email` and `AdminBootstrap:Password` configuration values.

## View the dashboard

Open **Dashboard** to see product count, total stock value, low-stock count, categories, suppliers, stock-in total, and the current low-stock alert list.

## Add suppliers and categories

Administrators: open **Inventory > Suppliers & Categories**, select **Add Supplier** or **Add Category**, enter the information, then select **Save**. Create these first so products can be assigned to both a supplier and category.

## Edit or delete suppliers and categories

Administrators: use **Edit** to change an existing supplier or category. Select **Delete**, then confirm in the dialog to permanently remove it.

## Add a new product

Administrators: open **Inventory > Product List** and select **+ Add**, or use **Product Form**. Enter the name, category, supplier, price, current stock, and minimum-stock threshold, then save.

## Edit or delete a product

Administrators: in **Product List**, use the pencil icon to edit a product. Use the trash icon to open the delete dialog, then select **Delete** to confirm the permanent removal.

## Record stock received

Open **Inventory > Stock In**, choose a product, enter the quantity and movement details, then select **Save Entry**. The product quantity increases and the movement is recorded.

## Record stock issued

Open **Inventory > Stock Out**, choose a product, enter the quantity and movement details, then select **Save Exit**. The product quantity decreases when sufficient stock is available.

## Review products and movement history

Use **Product List** to search products and filter them by category. Use **Inventory > Movements** to search the complete stock-in and stock-out history by product.

## Manage administrators

Administrators: open **Administrators** to create another administrator. Use **Make Admin** to grant administrator access or **Remove Admin** to revoke it; you cannot remove your own administrator role.

## Access levels and logout

Standard users can view the dashboard and work with stock movements. Administrator-only pages manage products, suppliers, categories, and administrator accounts. Select **Logout** in the top bar when finished.
