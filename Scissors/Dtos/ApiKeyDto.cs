using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Scissors.Models;

namespace Scissors.Dtos
{
    public class ApiKeyDto
    {
        /**
        * Auto-increment Id.
        */
        public int Id { get; set; }

        /**
         * Api Key için belirlenecek kullanıcı adı.
         */
        public string Username { get; set; }

        /**
         * Authentication ve/ya authorization için kullanıcak olan key.
         */
        public string AccessKey { get; set; }


        /**
         * Api Key'in geçerli olup olmadığı bilgisi.
         */
        public bool isActive { get; set; }

        /**
         * Api Key'in bir admin key olup olmadığı bilgisi.
         */
        public bool isAdmin { get; set; }

        /**
         * Kaç kez short url oluşturulmuş bilgisi.
         */
         public int Usage { get; set; }

        /**
         * Api Key'in oluşturulma tarihi.
         */
        public DateTime CreatedAt { get; set; }

        public static ApiKeyDto FromEntity (ApiKey apiKey)
        {
            return new ApiKeyDto() {
                Id = apiKey.Id,
                Username = apiKey.Username,
                AccessKey = apiKey.AccessKey,
                isActive = apiKey.isActive,
                isAdmin = apiKey.isAdmin,
                Usage = apiKey.ShortUrls.Count
            };
        }

        public static List<ApiKeyDto> FromEntityList (List<ApiKey> apiKeyList)
        {
            List<ApiKeyDto> dtoList = new List<ApiKeyDto>();

            foreach (var apiKey in apiKeyList)
                dtoList.Add(ApiKeyDto.FromEntity(apiKey));

            return dtoList;
        }

        public ApiKey ToEntity ()
        {
            var apiKey = new ApiKey()
            {
                Username = Username,
                isAdmin = isAdmin,
                isActive = isActive,
                AccessKey = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now
            };

            return apiKey;
        }
    }
}