using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using static Horeca.Shared.AuthUtils.PolicyProvider.PermissionAuthorizeAttribute;

namespace Horeca.Shared.AuthUtils.PolicyProvider
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options) { }

        /// <inheritdoc />
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                // it's not one of our dynamic policies, so we fallback to the base behavior
                // this will load policies added in Startup.cs (AddPolicy..)
                return await base.GetPolicyAsync(policyName);
            };
            // Will extract the Operator AND/OR enum from the string
            PermissionOperator @operator = GetOperatorFromPolicy(policyName);
            // Will extract the permissions from the string (Create, Update..)
            string[] permissions = GetPermissionsFromPolicy(policyName);
            string className = GetClassNameFromPolicy(policyName);
            // extract the info from the policy name and create our requirement
            var requirement = new PermissionRequirement(@operator, className, permissions);

            // create and return the policy for our requirement
            return new AuthorizationPolicyBuilder().AddRequirements(requirement).Build();
        }
    }
}