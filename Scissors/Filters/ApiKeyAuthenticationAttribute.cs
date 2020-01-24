using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Scissors.Services;
using System.Security.Principal;
using System.Security.Claims;

namespace Scissors.Filters
{
    public class ApiKeyAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private bool AdminRestricted;

        public ApiKeyAuthorizationAttribute(bool AdminRestricted)
        {
            this.AdminRestricted = AdminRestricted;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var accessKey = FetchFromHeader(actionContext);
            if (accessKey == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            var apiKey = ApiKeyService.FindByAccessKey(accessKey);
            if (apiKey == null) { 
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            if (!apiKey.isActive)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            if (AdminRestricted && !apiKey.isAdmin)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            var identity = new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Sid, "" + apiKey.Id),
                new Claim(ClaimTypes.UserData, apiKey.isAdmin ? "Admin" : "User")
            });

            IPrincipal user = new ClaimsPrincipal(identity);

            actionContext.RequestContext.Principal = user;

            base.OnAuthorization(actionContext);
        }

        private string FetchFromHeader (HttpActionContext actionContext)
        {
            string accessKey = null;

            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization != null && !string.IsNullOrEmpty(authorization.Scheme))
                accessKey = authorization.Scheme;

            return accessKey;
        }
    }
}