using Microsoft.AspNetCore.Http;
using Perficient.Infrastructure.Interfaces.Services;
using System;

namespace Perficient.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual string Get(string cookie)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }

            return _httpContextAccessor.HttpContext.Request.Cookies[cookie];
        }

        public virtual void Set(string cookie, string value, bool sessionCookie = false)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var options = new CookieOptions()
            {
                HttpOnly = true,
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps
            };

            if (!sessionCookie)
            {
                options.Expires = DateTime.Now.AddYears(1);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, value, options);
        }

        public virtual void Remove(string cookie)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var options = new CookieOptions()
            {
                HttpOnly = true,
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps,
                Expires = DateTime.Now.AddDays(-1),
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, "", options);
        }
    }
}
