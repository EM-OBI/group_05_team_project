using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fims.Models;
using fims.Models.ViewModels;

namespace fims.Components
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/api/auth");

            group.MapPost("/login", async (
                [FromBody] LoginViewModel model,
                SignInManager<ApplicationUser> signInManager,
                HttpContext context) =>
            {
                if (model is null ||
                    string.IsNullOrWhiteSpace(model.Email) ||
                    string.IsNullOrWhiteSpace(model.Password))
                {
                    return Results.BadRequest(new { message = "Email and password are required." });
                }

                var result = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    isPersistent: model.RememberMe,
                    lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Results.Json(new { message = "Invalid email or password." }, statusCode: StatusCodes.Status401Unauthorized);
                }

                var returnUrl = context.Request.Query["returnUrl"].ToString();
                if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith('/'))
                {
                    returnUrl = "/";
                }

                return Results.Ok(new { redirectUrl = returnUrl });
            });

            group.MapPost("/register", async (
                [FromBody] RegisterViewModel model,
                UserManager<ApplicationUser> userManager) =>
            {
                if (model is null ||
                    string.IsNullOrWhiteSpace(model.FullName) ||
                    string.IsNullOrWhiteSpace(model.Email) ||
                    string.IsNullOrWhiteSpace(model.Password))
                {
                    return Results.BadRequest(new { message = "All fields are required." });
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return Results.BadRequest(new { message = "Password and confirmation password do not match." });
                }

                var user = new ApplicationUser
                {
                    FullName = model.FullName.Trim(),
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim()
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                    return Results.BadRequest(new { message = errors });
                }

                return Results.Ok(new { message = "Account created successfully." });
            });

            endpoints.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Redirect("/");
            });
            // group.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            // {
            //     await signInManager.SignOutAsync();
            //     return Results.Ok(new { redirectUrl = "/" });
            // });

            var adminGroup = endpoints.MapGroup("/api/admin-users")
                .RequireAuthorization(policy => policy.RequireRole("Admin"));

            adminGroup.MapGet("/", async (UserManager<ApplicationUser> userManager) =>
            {
                var users = await userManager.Users.ToListAsync();
                var items = new List<AdminUserListItem>();

                foreach (var u in users)
                {
                    items.Add(new AdminUserListItem
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email ?? string.Empty,
                        IsAdmin = await userManager.IsInRoleAsync(u, "Admin")
                    });
                }

                return Results.Ok(items.OrderBy(i => i.FullName));
            });

            adminGroup.MapPost("/create", async (
                [FromBody] RegisterViewModel model,
                UserManager<ApplicationUser> userManager) =>
            {
                if (model is null ||
                    string.IsNullOrWhiteSpace(model.FullName) ||
                    string.IsNullOrWhiteSpace(model.Email) ||
                    string.IsNullOrWhiteSpace(model.Password))
                {
                    return Results.BadRequest(new { message = "All fields are required." });
                }

                if (model.Password != model.ConfirmPassword)
                {
                    return Results.BadRequest(new { message = "Password and confirmation password do not match." });
                }

                var user = new ApplicationUser
                {
                    FullName = model.FullName.Trim(),
                    UserName = model.Email.Trim(),
                    Email = model.Email.Trim()
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                    return Results.BadRequest(new { message = errors });
                }

                await userManager.AddToRoleAsync(user, "Admin");

                return Results.Ok(new { message = "Administrator created successfully." });
            });

            adminGroup.MapPost("/{id}/role", async (
                string id,
                [FromBody] SetAdminRoleCommand command,
                UserManager<ApplicationUser> userManager,
                HttpContext context) =>
            {
                var target = await userManager.FindByIdAsync(id);
                if (target == null)
                {
                    return Results.NotFound(new { message = "User not found." });
                }

                var isAdmin = await userManager.IsInRoleAsync(target, "Admin");

                if (command.IsAdmin && !isAdmin)
                {
                    await userManager.AddToRoleAsync(target, "Admin");
                    return Results.Ok(new { message = $"{target.FullName} is now an administrator." });
                }

                if (!command.IsAdmin && isAdmin)
                {
                    var currentUserId = userManager.GetUserId(context.User);
                    if (currentUserId == target.Id)
                    {
                        return Results.BadRequest(new { message = "You cannot remove your own administrator role." });
                    }

                    await userManager.RemoveFromRoleAsync(target, "Admin");
                    return Results.Ok(new { message = $"{target.FullName} is no longer an administrator." });
                }

                return Results.Ok(new { message = "No change was made." });
            });
        }
    }
}
