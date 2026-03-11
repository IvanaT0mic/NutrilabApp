using Microsoft.AspNetCore.Components;
using Nutrilab.Dtos.Permissions;
using Nutrilab.Dtos.Roles;
using NutrilabApp.Frontend.Services;

namespace NutrilabApp.Frontend.Pages.RoleManagement
{
    public class RoleManagementBase : PageBase
    {
        [Inject] protected RoleApiService RoleApiService { get; set; } = default!;

        protected List<RoleDto> Roles { get; set; } = new();
        protected List<PermissionDto> AllPermissions { get; set; } = new();

        protected bool IsLoading { get; set; } = true;
        protected bool IsSaving { get; set; } = false;
        protected bool IsAdmin { get; set; } = false;

        // Selected role for right panel
        protected RoleDto? SelectedRole { get; set; }
        protected List<int> EditingPermissionIds { get; set; } = new();
        protected bool IsEditingPermissions { get; set; } = false;

        // Inline new role (future use - for now just shows permissions)
        protected bool IsAddingRole { get; set; } = false;
        protected string NewRoleName { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.IsAuthenticatedAsync())
            {
                Navigation.NavigateTo("/login");
                return;
            }

            var roles = await TokenService.GetRolesAsync();
            if (!roles.Contains("Admin") && !roles.Contains("Maintainer"))
            {
                Navigation.NavigateTo("/dashboard");
                return;
            }

            IsAdmin = roles.Contains("Admin");
            await LoadData();
        }

        private async Task LoadData()
        {
            IsLoading = true;
            try
            {
                var rolesTask = RoleApiService.GetAllRolesAsync();
                var permTask = RoleApiService.GetAllPermissionsAsync();
                await Task.WhenAll(rolesTask, permTask);

                Roles = rolesTask.Result ?? new();
                AllPermissions = permTask.Result ?? new();

                // Re-sync selected role if it was already selected
                if (SelectedRole != null)
                {
                    SelectedRole = Roles.FirstOrDefault(r => r.Id == SelectedRole.Id);
                }
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected void SelectRole(RoleDto role)
        {
            SelectedRole = role;
            IsEditingPermissions = false;
            EditingPermissionIds = new();
        }

        protected void StartEditPermissions()
        {
            if (SelectedRole == null) return;

            EditingPermissionIds = AllPermissions
                .Where(p => SelectedRole.Permissions.Contains(p.Name))
                .Select(p => p.Id)
                .ToList();

            IsEditingPermissions = true;
        }

        protected void CancelEditPermissions()
        {
            IsEditingPermissions = false;
            EditingPermissionIds = new();
        }

        protected void TogglePermission(int permId, bool isChecked)
        {
            if (isChecked && !EditingPermissionIds.Contains(permId))
                EditingPermissionIds.Add(permId);
            else if (!isChecked)
                EditingPermissionIds.Remove(permId);
        }

        protected async Task SavePermissions()
        {
            if (SelectedRole == null) return;
            IsSaving = true;

            try
            {
                await ActionGuard.GuardAsync();
                var req = new UpdateRolePermissionsDto()
                {
                    PermissionIds = EditingPermissionIds
                };
                var success = await RoleApiService.UpdateRolePermissionsAsync(SelectedRole.Id, req);
                if (success)
                {
                    Notifications.ShowSuccess($"Permissions for '{SelectedRole.Name}' updated!");
                    IsEditingPermissions = false;
                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex.Message);
            }
            finally
            {
                IsSaving = false;
            }
        }

        protected bool HasPermission(string permName)
        {
            return SelectedRole?.Permissions.Contains(permName) ?? false;
        }

        protected string GetPermissionLabel(string name) => name switch
        {
            "user.manage.roles" => "Manage User Roles",
            "recipe.create" => "Create Recipes",
            "recipe.edit" => "Edit Recipes",
            "recipe.delete" => "Delete Recipes",
            "recipe.read" => "Read Recipes",
            "recipe.favourite" => "Favourite Recipes",
            _ => name
        };
    }
}