using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Extension for Http context
/// </summary>
namespace turradgiver_api.Utils
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// Return the userId from the httpContext.User object build by the authentication
        /// </summary>
        /// <param name="httpContext">The httpContext to extends with a method</param>
        /// <returns>Return the id of the user</returns>
        public static int GetUserId(this HttpContext httpContext ){
            if(httpContext.User == null){
                return -1;
            }

            return Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
