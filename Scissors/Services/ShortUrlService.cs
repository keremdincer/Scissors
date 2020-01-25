using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Scissors.Models;
using Scissors.Dtos;
using Scissors.Helpers;

namespace Scissors.Services
{
    public static class ShortUrlService
    {
        private static UrlShortenerEntities db = new UrlShortenerEntities();

        public static List<ShortUrlDto> FindAll(bool isAdmin) => ShortUrlDto.FromEntityList(db.ShortUrls.ToList());

        public static ShortUrl FindByUrl(string url) => db.ShortUrls.FirstOrDefault(x =>
            x.Url == url &&
            (x.isPermanent || x.ExpiresAt > DateTime.Now)
        );

        public static ShortUrl FindOne(int id, int apiKeyId, bool isAdmin) {
            var shortUrl = db.ShortUrls.FirstOrDefault(x => x.Id == id && (isAdmin || x.ApiKeyId == apiKeyId));

            if (shortUrl != null)
                return shortUrl;

            throw new Exception("Short Url record not found");
        }

        public static ShortUrl Create(ShortUrlDto dto)
        {
            // Eğer üretilmek istenen original url kalıcı bir bağlantıysa,
            // ve daha önce aynı original url için kalıcı bir bağlantı oluşturulmuşsa
            // yeni bir bağlantı oluşturmak yerine varolanı döndür.
            // Kullanıcı bazlı olarak!.

            var shortUrlRecord = db.ShortUrls.FirstOrDefault(x =>
                x.OriginalUrl == dto.OriginalUrl &&
                x.ApiKeyId == dto.ApiKeyId &&
                x.isPermanent == dto.isPermanent
            );

            if (shortUrlRecord != null)
                return shortUrlRecord;


            var newShortUrl = new ShortUrl() {
                OriginalUrl = dto.OriginalUrl,
                Url = UniqueId.Generate(),
                isPermanent = dto.isPermanent,
                CreatorId = dto.CreatorId,
                ApiKeyId = dto.ApiKeyId,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(dto.Expiration)
            };

            db.ShortUrls.Add(newShortUrl);
            db.SaveChanges();

            return newShortUrl;
        }

        public static ShortUrl Update(ShortUrlDto dto, bool isAdmin)
        {
            var shortUrlRecord = db.ShortUrls.FirstOrDefault(x =>
                x.Id == dto.Id &&
                (isAdmin || x.ApiKeyId == dto.ApiKeyId)
            );

            if (shortUrlRecord == null)
                throw new Exception("Short Url record not found");

            shortUrlRecord.isPermanent = dto.isPermanent;
            shortUrlRecord.ExpiresAt = dto.ExpiresAt;

            db.SaveChanges();

            return shortUrlRecord;
        }

        public static void Delete(int id, int apiKeyId, bool isAdmin)
        {
            var shortUrl = db.ShortUrls.FirstOrDefault(x => x.Id == id && (isAdmin || x.ApiKeyId == apiKeyId));

            if (shortUrl == null)
                throw new Exception("Short Url record not found");

            db.ShortUrls.Remove(shortUrl);

            db.SaveChanges();
        }

        public static void AddUsage(ShortUrlUsageDto dto)
        {
            var shortUrl = db.ShortUrls.Find(dto.ShortUrlId);

            shortUrl.ShortUrlUsages.Add(new ShortUrlUsage() {
                ClientIP = dto.ClientIP,
                ClientBrowser = dto.ClientBrowser,
                ClientDevice = dto.ClientDevice,
                ClientOS = dto.ClientOS,
                CreatedAt = DateTime.Now
            });

            db.SaveChanges();
        }
    }
}