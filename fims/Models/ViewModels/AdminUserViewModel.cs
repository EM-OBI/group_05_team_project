namespace fims.Models.ViewModels;

public class AdminUserListItem
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}

public class SetAdminRoleCommand
{
    public bool IsAdmin { get; set; }
}