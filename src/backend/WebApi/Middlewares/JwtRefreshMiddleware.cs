using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.BLL.Helpers;
using WebApi.BLL.Services.Interfaces.Auth;
using WebApi.BLL.Services.Interfaces.Cache;

namespace WebApi.Middlewares;

public class JwtRefreshMiddleware
{
    private readonly RequestDelegate _next;

    public JwtRefreshMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService authService)
    {
        var accessToken = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

        bool needRefresh = false;

        if (!string.IsNullOrEmpty(accessToken))
        {
            try
            {
                JwtHelper.ValidateToken(accessToken);
            }
            catch
            {
                needRefresh = true;
            }
        }

        if (needRefresh)
        {
            var refreshToken = context.Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                try
                {
                    var tokens = await authService.RefreshToken(refreshToken);

                    context.Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(7)
                    });

                    context.Request.Headers["Authorization"] = $"Bearer {tokens.AccessToken}";
                }
                catch
                {
                }
            }
        }

        await _next(context);
    }
}