using CompanyDAL.EF;
using CompanyDAL.Repos;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Text;

namespace MyTaskApp.Infrastracture.Services
{
    public sealed class UrlShortener
    {
        private readonly UrlDbContext _context;

        public UrlShortener(UrlDbContext context)
        {
            _context = context;
        }

        public async Task<String?> GenerateShortUrlAsync(ShortUrl shortUrl, HttpRequest request)
        {
            if (!Uri.TryCreate(shortUrl.Url, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                return null;
            }

            var existing = await _context.ShortUrls
                .Include(x => x.ShortUrlInfo)
                .FirstOrDefaultAsync(x => x.Url == shortUrl.Url);

            if (existing != null)
            {
                return existing.Short;
            }

            shortUrl.Short = "";
            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            String code = Encode(shortUrl.Id);
            
            shortUrl.Short = code;
            await _context.SaveChangesAsync();


            String shortLink = $"{request.Scheme}://{request.Host}/r/{code}";

            return shortLink;
        }

        private String Encode(Int32 num)
        {
            const String alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (num == 0) return alphabet[0].ToString();

            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, alphabet[num % alphabet.Length]);
                num /= alphabet.Length;
            }
            return sb.ToString();
        }


        public async Task<String?> GetlUrl(String shortCode)
        {
            return await _context.ShortUrls
                        .Where(x => x.Short == shortCode)
                        .Select(x => x.Url)
                        .FirstOrDefaultAsync();
        }

    }
}
