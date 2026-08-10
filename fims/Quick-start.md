# FIMS Quick Start

FIMS (Five's Inventory Management System) is a web app for tracking products, stock movements, suppliers, and categories.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Run the app

From the project root, run:

```bash
dotnet restore
dotnet run
```

Open the URL printed in the terminal (normally `https://localhost:7193` or `http://localhost:5235`). On first launch, FIMS creates or updates the SQLite database (`fims.db`) and seeds example suppliers and categories.

## Sign in

Use the default administrator account:

```text
Email:    admin@fims.local
Password: Admin@12345
```

Change these bootstrap credentials before deploying by setting `AdminBootstrap:Email` and `AdminBootstrap:Password` in configuration or environment variables.

## First tasks

1. Sign in, then open **Inventory** in the navigation menu.
2. As an administrator, use **Suppliers & Categories** to add reference data.
3. Use **Product Form** to add products and stock thresholds.
4. Record deliveries in **Stock In** and withdrawals in **Stock Out**.
5. Review current quantities in **Product List**, movement activity in **Movements**, and alerts on the **Dashboard**.

Newly registered accounts can use stock and movement pages. Administrator-only pages manage products, suppliers, categories, and other administrators.
