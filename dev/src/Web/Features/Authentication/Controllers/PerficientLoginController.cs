using EPiServer.Core;
using EPiServer.Shell.Security;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perficient.Web.Features.Authentication.ViewModels;
using System;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Authentication.Controllers
{
    [Route("PLogin")]
    public class PerficientLoginController : Controller
    {
        private readonly UISignInManager _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PerficientLoginController(UISignInManager signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException((nameof(signInManager)));
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult Index()
        {
            return View("/Features/Authentication/Views/Index.cshtml", new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signInSuccess = await _signInManager.SignInAsync(model.Username, model.Password);

                if (signInSuccess)
                {
                    var returnUrl = _httpContextAccessor.HttpContext.Request.Query["ReturnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return Redirect("/Episerver/Cms");
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("LoginError", "Login failed");

            return View("/Features/Authentication/Views/Index.cshtml", model);
        }

        [HttpGet]
        [Route("PLogin/Logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return Redirect(UrlResolver.Current.GetUrl(ContentReference.StartPage));
        }
    }
}
