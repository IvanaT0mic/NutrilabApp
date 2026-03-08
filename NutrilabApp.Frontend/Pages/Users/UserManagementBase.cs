using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Roles;
using Nutrilab.Dtos.Users;
using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RoleServices;
using NutrilabApp.Frontend.Services.UserServices;

namespace NutrilabApp.Frontend.Pages.Users
{
    public class UserManagementBase : ComponentBase
    {
        [Inject] protected UserApiService UserApiService { get; set; } = default!;
        [Inject] protected RoleApiService RoleApiService { get; set; } = default!;
        [Inject] protected TokenService TokenService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected List<UserDto> Users { get; set; } = new();
        protected List<RoleDto> AllRoles { get; set; } = new();

        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;
        protected bool CanDelete { get; set; } = false;

        protected string ErrorMessage { get; set; } = "";
        protected string SuccessMessage { get; set; } = "";

        // Edit state
        protected long? EditingUserId { get; set; }
        protected List<int> EditingRoleIds { get; set; } = new();

        // Delete confirm
        protected bool ShowDeleteConfirm { get; set; } = false;
        protected long DeleteTargetId { get; set; }
        protected string DeleteTargetEmail { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            var roles = await TokenService.GetRolesAsync();
            bool isAdminOrMaintainer = roles.Contains("Admin") || roles.Contains("Maintainer");

            if (!isAdminOrMaintainer)
            {
                Navigation.NavigateTo("/dashboard");
                return;
            }

            CanDelete = roles.Contains("Admin");

            await LoadData();
        }

        private async Task LoadData()
        {
            IsLoading = true;
            ErrorMessage = "";
            try
            {
                var usersTask = UserApiService.GetAllUsersAsync();
                var rolesTask = RoleApiService.GetAllRolesAsync();
                await Task.WhenAll(usersTask, rolesTask);

                Users = usersTask.Result ?? new();
                AllRoles = rolesTask.Result ?? new();
            }
            catch
            {
                ErrorMessage = "Failed to load users. Check your connection.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected void StartEdit(UserDto user)
        {
            EditingUserId = user.Id;
            EditingRoleIds = AllRoles
                .Where(r => user.Roles.Contains(r.Name))
                .Select(r => r.Id)
                .ToList();
            SuccessMessage = "";
            ErrorMessage = "";
        }

        protected void ToggleRole(int roleId, bool isChecked)
        {
            if (isChecked && !EditingRoleIds.Contains(roleId))
                EditingRoleIds.Add(roleId);
            else if (!isChecked)
                EditingRoleIds.Remove(roleId);
        }

        protected void CancelEdit()
        {
            EditingUserId = null;
            EditingRoleIds = new();
        }

        protected async Task SaveRoles()
        {
            if (EditingUserId == null) return;
            IsSaving = true;
            ErrorMessage = "";

            var success = await UserApiService.UpdateUserRolesAsync(EditingUserId.Value, EditingRoleIds);

            if (success)
            {
                SuccessMessage = "Roles updated successfully!";
                EditingUserId = null;
                await LoadData();
            }
            else
            {
                ErrorMessage = "Failed to update roles.";
            }

            IsSaving = false;
        }

        protected void ConfirmDelete(long userId, string email)
        {
            DeleteTargetId = userId;
            DeleteTargetEmail = email;
            ShowDeleteConfirm = true;
        }

        protected void CancelDelete()
        {
            ShowDeleteConfirm = false;
        }

        protected async Task ExecuteDelete()
        {
            IsSaving = true;
            var success = await UserApiService.DeleteUserAsync(DeleteTargetId);

            if (success)
            {
                SuccessMessage = $"User {DeleteTargetEmail} deleted.";
                ShowDeleteConfirm = false;
                await LoadData();
            }
            else
            {
                ErrorMessage = "Failed to delete user. They may have recipes.";
                ShowDeleteConfirm = false;
            }
            IsSaving = false;
        }
    }
}
