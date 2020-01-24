using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

using Scissors.Services;
using Scissors.Filters;
using Scissors.Dtos;
using System.Web.Http.Description;

namespace Scissors.Controllers
{
    public class ShortUrlsController : ApiController
    {
        [ApiKeyAuthorization(AdminRestricted: false)]
        [ResponseType(typeof(List<ShortUrlDto>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var principal = (ClaimsPrincipal)User;

            var apiKeyId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            var isAdmin = principal.Claims.First(x => x.Type == ClaimTypes.UserData).Value == "Admin";

            try
            {
                var shortUrls = ShortUrlService.FindAll(isAdmin);
                return request.CreateResponse(HttpStatusCode.OK, shortUrls);
            }
            catch(Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: false)]
        [ResponseType(typeof(ShortUrlDto))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var principal = (ClaimsPrincipal)User;
            var apiKeyId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            var isAdmin = principal.Claims.First(x => x.Type == ClaimTypes.UserData).Value == "Admin";

            try
            {
                return request.CreateResponse(HttpStatusCode.OK, ShortUrlService.FindOne(id, apiKeyId, isAdmin));
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: false)]
        [ResponseType(typeof(ShortUrlDto))]
        public HttpResponseMessage Post(HttpRequestMessage request, ShortUrlDto dto)
        {
            var principal = (ClaimsPrincipal)User;
            var apiKeyId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value);

            dto.ApiKeyId = apiKeyId;

            try
            {
                return request.CreateResponse(HttpStatusCode.Created, ShortUrlService.Create(dto));
            }
            catch(Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: false)]
        [ResponseType(typeof(ShortUrlDto))]
        public HttpResponseMessage Put(HttpRequestMessage request, ShortUrlDto dto)
        {
            var principal = (ClaimsPrincipal)User;
            var apiKeyId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            var isAdmin = principal.Claims.First(x => x.Type == ClaimTypes.UserData).Value == "Admin";

            dto.ApiKeyId = apiKeyId;

            try
            {
                return request.CreateResponse(HttpStatusCode.OK, ShortUrlService.Update(dto, isAdmin));
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: false)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var principal = (ClaimsPrincipal)User;
            var apiKeyId = int.Parse(principal.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            var isAdmin = principal.Claims.First(x => x.Type == ClaimTypes.UserData).Value == "Admin";

            try
            {
                ShortUrlService.Delete(id, apiKeyId, isAdmin);

                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
