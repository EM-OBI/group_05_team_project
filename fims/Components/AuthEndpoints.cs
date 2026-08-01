using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    return Results.Unauthorized();
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

            group.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok(new { redirectUrl = "/" });
            });
        }
    }
}
