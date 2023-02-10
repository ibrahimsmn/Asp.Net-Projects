using GameNews.Context;
using GameNews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameNews.Controllers
{
    public class AuthController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        
        readonly SignInManager<AppUser> _signinManager;
        

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            
        }
        public  IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
              return  RedirectToAction("index", "home");
            }
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInViewModel model)
            {
            

            if (ModelState.IsValid)
            {
                AppUser user;
                if (model.UserNameOrEmail.Contains("@"))
                {
                    
                    user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                }
                else
                {
                  
                    user = await _userManager.FindByNameAsync(model.UserNameOrEmail);
                }
                if (user == null)
                {
                    ModelState.AddModelError("","Kullanıcı Bilgileri Yanlış!!");
                    return View(model);
                }
                var result = await _signinManager.PasswordSignInAsync(user,model.Password, false, false);
                if (result.Succeeded)
                {
                    TempData["UserID"] = user.Id;
                    
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("","Kullanıcı Bilgileri Yanlış!!");
                    return View(model);
                }
            }
           

            return View(model);
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Mail,
                    ImageUrl ="defaulphoto2.png"
                    
                };
                

                    var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    AppUser userr = await _userManager.FindByNameAsync(user.UserName);
                    await _userManager.AddToRoleAsync(userr, "member");
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                
            }
            return View(model);
        }
        public async Task<IActionResult> SignOut()
        {
           
            await _signinManager.SignOutAsync();
            return RedirectToAction("index","home");
        }
    }
}
