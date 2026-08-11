using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.HttpOverrides;
using fims.Components;
using fims.Data;
using fims.Models;
using fims.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies(options =>
    {
        options.ApplicationCookie!.Configure(c =>
        {
            c.LoginPath = "/login";
            c.AccessDeniedPath = "/access-denied";
        });
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

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

    await EnsureAdminRoleAndUserAsync(scope,
        builder.Configuration["AdminBootstrap:Email"] ?? "admin@fims.local",
        builder.Configuration["AdminBootstrap:Password"] ?? "Admin@12345");
}

static async Task EnsureAdminRoleAndUserAsync(IServiceScope scope, string adminEmail, string adminPassword)
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    const string adminRole = "Admin";
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser is null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Administrator"
        };
        var createResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
    else if (!await userManager.IsInRoleAsync(adminUser, adminRole))
    {
        await userManager.AddToRoleAsync(adminUser, adminRole);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/notfound", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapAuthEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
