using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using aula_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using aula_5.Views;

using aula_5.Context;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace aula_5.Controllers
{
    public class UsuarioController : Controller
    {
        /* PARTE TENTATIVA DE ARRUMAR BUGS MAS FOI DESNECESSÁRIO (COPIEI DE "AlunoController")
        private readonly MyContext _MyContext;

        public UsuarioController(MyContext myContext)
        {
            this._MyContext = myContext;
        } */
        
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<Usuario> _logger;

        //
        public UsuarioController(UserManager<Usuario> userManager,
                                 SignInManager<Usuario> signInManager,
                                 ILogger<Usuario> logger    )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
           
                ViewBag.ReturnUrl = returnUrl;
                return View();
           
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,
                                               string returnUrl = null)
        {
                //
                ViewBag.ReturnUrl = returnUrl;


                if (ModelState.IsValid)
                {
                    //
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha,
                                                                          model.flManterLogado, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //
                        _logger.LogInformation("Usuario Autenticado!");
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, model.Email) // Substitua "nomeUsuario" pelo nome do usuário logado
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        TempData["type"] = "success";
                        TempData["title"] = "Login:";
                        TempData["body"] = "Usuario entrou com sucesso!";
                        
                        // Redirecione para a página desejada após o login bem-sucedido
                        return RedirectToAction("Index", "Home");

                        //return RedirectToLocal(returnUrl);
                    }
                }
                //aqui seria um else?
                ModelState.AddModelError(string.Empty, "Falha na tentativa de login!");
                
                //esse seria fora do else?
                return View(model);
        }

        //aqui o ChatGPT implementou o Logout()

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public IActionResult RegistrarNovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario(NovoLoginViewModel model,
                                                              string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Usuario{UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário criou uma nova conta com senha!");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("Usuário acesso com a conta criada!");

                        TempData["type"] = "success";
                        TempData["title"] = "Logon:";
                        TempData["body"] = "Usuario cadastrado com sucesso!";

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors){
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuário realizou Logout!");
            return RedirectToAction("Login", "Usuario");
        }
    }
}