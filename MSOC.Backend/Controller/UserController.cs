﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MSOC.Backend.Controller;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        // If logged in, why even bother?
        if (User.Identity?.IsAuthenticated ?? true) return Redirect("/api/user/identity");

        return Challenge(new AuthenticationProperties { RedirectUri = "/api/user/identity" }, "Discord");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        // Not logged in.
        if (!User.Identity?.IsAuthenticated ?? true) return Redirect("/api/healthcheck");

        return SignOut(
            new AuthenticationProperties { RedirectUri = "/api/healthcheck" },
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }

    [HttpGet("identity")]
    public IActionResult ShowIdentity()
    {
        // Not logged in.
        if (!User.Identity?.IsAuthenticated ?? true) return Redirect("/api/user/login");

        return new JsonResult(new Dictionary<string, string>
        {
            ["username"] = User.Identity?.Name ?? "anon",
            ["auth_type"] = User.Identity?.AuthenticationType ?? "none",
            ["user_id"] = User.Claims.ElementAt(0).Value,
            ["avatar_url"] = User.Claims.ElementAt(4).Value
        });
    }
}