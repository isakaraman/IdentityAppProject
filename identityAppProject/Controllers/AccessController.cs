using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace identityAppProject.Controllers
{
    [Authorize]
    public class AccessController : Controller
    {
        //Authorize from cookie/jwt
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Pokemon,Trainer")]
        public IActionResult PokemonAndTrainerAccess()
        {
            return View();
        }
        [Authorize(Policy ="OnlyTrainerChecker")]
        public IActionResult OnlyTrainerChecker()
        {
            return View();
        }
        [Authorize(Policy ="CheckNicknameTeddy")]
        public IActionResult CheckNicknameTeddy() 
        { 
            return View();
        }
    }
}
