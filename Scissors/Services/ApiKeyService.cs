using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Scissors.Models;
using Scissors.Dtos;

namespace Scissors.Services
{
    public static class ApiKeyService
    {
        private static UrlShortenerEntities db = new UrlShortenerEntities();

        public static List<ApiKeyDto> FindAll () => ApiKeyDto.FromEntityList(db.ApiKeys.ToList());

        public static ApiKey FindOne(int id) => db.ApiKeys.Find(id);

        public static ApiKey FindByAccessKey (string accessKey) =>
            db.ApiKeys.FirstOrDefault(x => x.AccessKey == accessKey
        );

        public static ApiKey Create (ApiKeyDto dto)
        {
            var newApiKey = dto.ToEntity();
            db.ApiKeys.Add(newApiKey);

            db.SaveChanges();
            return newApiKey;
        }

        public static ApiKey Update (ApiKeyDto dto)
        {
            var apiKeyRecord = db.ApiKeys.Find(dto.Id);

            if (apiKeyRecord == null)
                throw new Exception("Api Key record not found");

            apiKeyRecord.Username = dto.Username;
            apiKeyRecord.isActive = dto.isActive;
            apiKeyRecord.isAdmin = dto.isAdmin;

            db.SaveChanges();

            return apiKeyRecord;
        }

        public static void Delete (int id)
        {
            var apiKeyRecord = db.ApiKeys.Find(id);

            if (apiKeyRecord == null)
                throw new Exception("Api Key record not found");

            db.ApiKeys.Remove(apiKeyRecord);

            db.SaveChanges();
        }
    }
}