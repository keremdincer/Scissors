using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Scissors.Models;

namespace Scissors.Dtos
{
    public class ShortUrlDto
    {
        /**
         * Auto-increment Id.
         */
        public int Id { get; set; }

        /**
         * Kısaltılmış olan orijinal url.
         */
        public string OriginalUrl { get; set; }

        /**
         * Sistemin üreteceği kısaltılmış url.
         */
        public string Url { get; set; }

        /**
         * Üretilen url'in kalıcı (son kullanma tarihi) olup olmadığı.
         */
        public bool isPermanent { get; set; }

        /**
         * Kısa url oluşturulurken controller'a gönderilecek geçerlilik süresi (gün bazlı).
         */
        public int Expiration { get; set; }

        /**
         * ShortUrl'in kaç kez kullanıldığı bilgisi.
         */
        public int Usage { get; set; }

        /**
         * Zorunlu olmamakla birlikte eğer API'yi kullanan farklı bir uygulama üzerinde
         * yeni kısa url'i kimin oluşturduğunun bilgisi tutulmak istenirse burada Id bazlı
         * log'lama yapılabilir.
         */
        public int? CreatorId { get; set; }


        /**
         * Bu ShortUrl hangi api key kullanılarak oluşturuldu bilgisi.
         */
        public int ApiKeyId { get; set; }

        /**
         * Kısa url'in oluşturulduğu tarih.
         */
        public DateTime CreatedAt { get; set; }

        /**
         * Kısa url'in son kullanma tarihi.
         */
        public DateTime? ExpiresAt { get; set; }

        public static ShortUrlDto FromEntity (ShortUrl shortUrlEntity)
        {
            return new ShortUrlDto() {
                Id = shortUrlEntity.Id,
                OriginalUrl = shortUrlEntity.OriginalUrl,
                Url = shortUrlEntity.Url,
                CreatorId = shortUrlEntity.CreatorId,
                ApiKeyId = shortUrlEntity.ApiKeyId,
                CreatedAt = shortUrlEntity.CreatedAt,
                ExpiresAt = shortUrlEntity.ExpiresAt,
                Usage = shortUrlEntity.ShortUrlUsages.Count,
                isPermanent = shortUrlEntity.isPermanent
            };
        }

        public static List<ShortUrlDto> FromEntityList (List<ShortUrl> shortUrlList)
        {
            List<ShortUrlDto> dtoList = new List<ShortUrlDto>();

            foreach (var shortUrl in shortUrlList)
                dtoList.Add(ShortUrlDto.FromEntity(shortUrl));

            return dtoList;
        }
    }
}