using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Scissors.Dtos;
using Scissors.Filters;
using Scissors.Services;

namespace Scissors.Controllers
{
    public class ApiKeysController : ApiController
    {
        [ApiKeyAuthorization(AdminRestricted: true)]
        [ResponseType(typeof(IEnumerable<ApiKeyDto>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var apiKeys = ApiKeyService.FindAll();
            return request.CreateResponse(HttpStatusCode.OK, apiKeys);
        }

        [ApiKeyAuthorization(AdminRestricted: true)]
        [ResponseType(typeof(ApiKeyDto))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var apiKey = ApiKeyService.FindOne(id);
            if (apiKey == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            return request.CreateResponse(HttpStatusCode.OK, apiKey);
        }

        [ApiKeyAuthorization(AdminRestricted: true)]
        [ResponseType(typeof(ApiKeyDto))]
        public HttpResponseMessage Post(HttpRequestMessage request, ApiKeyDto dto)
        {
            try
            {
                var newApiKey = ApiKeyService.Create(dto);
                return request.CreateResponse(HttpStatusCode.Created, newApiKey);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: true)]
        [ResponseType(typeof(ApiKeyDto))]
        public HttpResponseMessage Put(HttpRequestMessage request, ApiKeyDto dto)
        {
            try
            {
                var updatedApiKey = ApiKeyService.Update(dto);
                return request.CreateResponse(HttpStatusCode.OK, updatedApiKey);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [ApiKeyAuthorization(AdminRestricted: true)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                ApiKeyService.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
