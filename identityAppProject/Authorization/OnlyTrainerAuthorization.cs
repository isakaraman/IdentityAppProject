using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identityAppProject.Authorization
{
    public class OnlyTrainerAuthorization : AuthorizationHandler<OnlyTrainerAuthorization>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyTrainerAuthorization requirement)
        {
            if (context.User.IsInRole("Trainer"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
