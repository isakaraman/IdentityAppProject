using identityAppProject.Data;
using identityAppProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace identityAppProject.Authorization
{
    public class NicknameAuthorization : AuthorizationHandler<NicknameRequirment>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _db;

        public NicknameAuthorization(UserManager<AppUser> userManager,ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NicknameRequirment requirement)
        {
            string userid = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=_db.AppUser.FirstOrDefault(u=>u.Id==userid);
            var claims = Task.Run(async () => await _userManager.GetClaimsAsync(user)).Result;
            var claim = claims.FirstOrDefault(c => c.Type == "Nickname");
            if(claim!= null)
            {
                if(claim.Value.ToLower().Contains(requirement.Name.ToLower()))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;  
                }
            }
            return Task.CompletedTask;
        }
    }
}
