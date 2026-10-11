using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using webclient.Models;
using webclient.Modules;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webclient.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly WebApi _wa;

        public AccountController(
            ILogger<AccountController> logger,
            WebApi wa)
        {
            _logger = logger;
            _wa = wa;
        }


        // ============================================================
        // LOGIN
        // ============================================================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel lm)
        {
            if (!ModelState.IsValid)
            {
                return View(lm);
            }

            try
            {
                var data = await _wa.Response(
                    lm,
                    "api/account/login",
                    "POST");

                if (string.IsNullOrWhiteSpace(data))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Invalid username or password.");

                    return View(lm);
                }

                var result =
                    JsonConvert.DeserializeObject<UserProfileModel>(
                        data);

                if (result == null ||
                    string.IsNullOrWhiteSpace(result.Token))
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Invalid username or password.");

                    return View(lm);
                }


                // ====================================================
                // CREATE WEB CLIENT COOKIE IDENTITY
                // ====================================================

                var userClaims = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.Name,
                        result.UserName ?? ""),

                    new Claim(
                        ClaimTypes.NameIdentifier,
                        result.UserId ?? ""),

                    new Claim(
                        "itemid",
                        result.itemid ?? ""),

                    new Claim(
                        "usertype",
                        result.usertype ?? ""),

                    // JWT used when WebApi calls the backend
                    new Claim(
                        "access_token",
                        result.Token)
                };


                var identity = new ClaimsIdentity(
                    userClaims,
                    "ApplicationCookie");

                //SetRefreshCookie(result.RefreshToken);
                var principal =
                    new ClaimsPrincipal(identity);


                var props =
                    new AuthenticationProperties
                    {
                        IsPersistent = lm.RememberMe
                    };


                if (!string.IsNullOrWhiteSpace(
                        result.ExpiredCtrl) &&
                    int.TryParse(
                        result.ExpiredCtrl,
                        out var minutes))
                {
                    props.ExpiresUtc =
                        DateTimeOffset.UtcNow.AddMinutes(minutes);
                }


                await HttpContext.SignInAsync(
                    "ApplicationCookie",
                    principal,
                    props);


                return RedirectToAction(
                    "Index",
                    "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Login failed for user {Username}",
                    lm.username);

                ModelState.AddModelError(
                    string.Empty,
                    "Unable to login. Please try again.");

                return View(lm);
            }
        }


        // ============================================================
        // CHANGE PASSWORD
        // ============================================================

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new PasswordModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(
            PasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.password != model.confirmpassword)
            {
                ViewBag.message = "notsame";
                return View(model);
            }

            try
            {
                var data = await _wa.Response(
                    model,
                    "api/account/changepassword",
                    "");

                // Depending on your API response,
                // you should check whether the operation succeeded.

                return RedirectToAction(
                    nameof(ChangePassword));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Change password failed for user {UserId}",
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier));

                ViewBag.message = "error";

                return View(model);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue(
                    "refreshToken",
                    out var refreshToken))
            {
                return Unauthorized();
            }

            var ip =
                HttpContext.Connection
                    .RemoteIpAddress?
                    .ToString();
            RefreshModel rm = new RefreshModel();
            rm.refreshtoken = refreshToken;
            rm.ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var data = await _wa.Response(
                     rm,
                     "api/account/refreshtoken",
                     "");
            var result =
                    JsonConvert.DeserializeObject<RefreshModel>(
                        data);
            if (result == null)
            {
                DeleteRefreshCookie();

                return Unauthorized();
            }
            SetRefreshCookie(
                result.refreshtoken);

            return Ok(new
            {
                accessToken =
                    result.AccessToken
            });
        }
        private void SetRefreshCookie(
        string? refreshToken)
        {
            Response.Cookies.Append(
                "refreshToken",
                refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,

                    Secure = true,

                    SameSite =
                        SameSiteMode.Strict,

                    Expires =
                        DateTimeOffset.UtcNow
                            .AddDays(7),

                    IsEssential = true,

                    Path = "/"
                });
        }
        private void DeleteRefreshCookie()
        {
            Response.Cookies.Delete(
                "refreshToken");
        }
        // ============================================================
        // LOGOUT
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                "ApplicationCookie");

            return RedirectToAction(
                nameof(Login));
        }
    }
}

