using Microsoft.EntityFrameworkCore;
using fims.Components;
using fims.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new fims.Models.Category { Name = "Bakery" },
            new fims.Models.Category { Name = "Dairy" },
            new fims.Models.Category { Name = "Grains" },
            new fims.Models.Category { Name = "Beverages" },
            new fims.Models.Category { Name = "Produce" }
        );
        db.Suppliers.AddRange(
            new fims.Models.Supplier { Name = "Acme Foods", ContactInfo = "555-2211 / sales@acme.com" },
            new fims.Models.Supplier { Name = "Fresh Market", ContactInfo = "555-3344 / info@fresh.com" },
            new fims.Models.Supplier { Name = "Food Company", ContactInfo = "555-7788 / contact@food.com" }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
