using Microsoft.AspNetCore.Authorization;

namespace Horeca.Shared.AuthUtils.PolicyProvider
{
    public enum PermissionOperator
    {
        And = 1,
        Or = 2
    }

    /// <summary>
    /// We have the PermissionAuthorizeAttribute which is what we use to annotate our endpoints with the proper permissions
    /// </summary>
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION_";
        private const string Separator = "_";

        /// <summary>
        /// Initializes the attribute with multiple permissions
        /// </summary>
        /// <param name="permissionOperator">The operator to use when verifying the permissions provided</param>
        /// <param name="permissions">The list of permissions</param>
        public PermissionAuthorizeAttribute(PermissionOperator permissionOperator, string className, params string[] permissions)
        {
            // E.g: PERMISSION_1_Unit_Create_Update..
            Policy = $"{PolicyPrefix}{(int)permissionOperator}{Separator}{className}{Separator}{string.Join(Separator, permissions)}";
        }

        /// <summary>
        /// Initializes the attribute with a single permission
        /// </summary>
        /// <param name="permission">The permission</param>
        public PermissionAuthorizeAttribute(string className, string permission)
        {
            // E.g: PERMISSION_1_Unit_Create..
            Policy = $"{PolicyPrefix}{(int)PermissionOperator.And}{Separator}{className}{Separator}{permission}";
        }

        public static PermissionOperator GetOperatorFromPolicy(string policyName)
        {
            var @operator = int.Parse(policyName.AsSpan(PolicyPrefix.Length, 1));
            return (PermissionOperator)@operator;
        }

        public static string GetClassNameFromPolicy(string policyName)
        {
            return policyName[(PolicyPrefix.Length + 2)..]
                .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static string[] GetPermissionsFromPolicy(string policyName)
        {
            return policyName[(PolicyPrefix.Length + 2)..]
                .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
        }
    }
}