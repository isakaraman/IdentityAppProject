using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identityAppProject.Authorization
{
    public class NicknameRequirment : IAuthorizationRequirement
    {
        public NicknameRequirment(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
