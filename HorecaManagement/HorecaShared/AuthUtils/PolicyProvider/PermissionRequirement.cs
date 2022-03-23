using Microsoft.AspNetCore.Authorization;

namespace Horeca.Shared.AuthUtils.PolicyProvider
{
    /// <summary>
    /// We have the PermissionRequirement which is where we have the permission(s) and/or operator.
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public static string ClaimType => AppClaimTypes.Permissions;

        public PermissionOperator PermissionOperator { get; }

        public string[] Permissions { get; }

        public string ClassName { get; }

        public PermissionRequirement(PermissionOperator permissionOperator, string className, string[] permissions)
        {
            if (permissions.Length == 0)
                throw new ArgumentException("At least one permission is required.", nameof(permissions));

            PermissionOperator = permissionOperator;
            ClassName = className;
            Permissions = permissions;
        }
    }
}