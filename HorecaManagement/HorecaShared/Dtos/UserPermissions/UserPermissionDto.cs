namespace Horeca.Shared.Dtos.UserPermissions
{
    public class UserPermissionDto
    {
    }

    public class MutateUserPermissionsDto
    {
        public List<int> PermissionIds { get; set; }
        public string UserName { get; set; }
    }
}