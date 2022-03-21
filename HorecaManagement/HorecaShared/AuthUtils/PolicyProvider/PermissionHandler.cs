using Microsoft.AspNetCore.Authorization;

namespace Horeca.Shared.AuthUtils.PolicyProvider
{/// <summary>
/// We have the PermissionHandler which is where we receive our requirement instance and do our authz logic
/// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (requirement.PermissionOperator == PermissionOperator.And)
            {
                foreach (var permission in requirement.Permissions)
                {
                    if (!context.User.HasClaim(PermissionRequirement.ClaimType, $"{requirement.ClassName}_{permission}"))
                    {
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }

                // identity has all required permissions
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            foreach (var permission in requirement.Permissions)
            {
                if (context.User.HasClaim(PermissionRequirement.ClaimType, $"{requirement.ClassName}_{permission}"))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            // identity does not have any of the required permissions
            context.Fail();
            return Task.CompletedTask;
        }
    }
}