using CompanyDAL.EF;
using CompanyDAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using MyTaskApp.Infrastracture.Services;

namespace MyTaskApp.Controllers
{
    
    public sealed class TableController : Controller
    {
        private UrlDbContext _context { get; set; }
        private UrlShortener _shortenerService { get; set; }
        private Repo<ShortUrl> _shortUrlRepo { get; set; }
        private Repo<ShortUrlInfo> _shortUrlInfoRepo { get; set; }

        public TableController(UrlDbContext dbContext) {

            _shortenerService = new UrlShortener(dbContext);
            _shortUrlRepo = new Repo<ShortUrl>(dbContext);
            _shortUrlInfoRepo = new Repo<ShortUrlInfo>(dbContext);
            _context = dbContext;
        }

        public ActionResult Index()
        {
            IEnumerable<ShortUrl> list = _shortUrlRepo.List(new QueryOptions<ShortUrl>());
            return View("ShortUrl", list);
        }
        [Authorize]
        public ActionResult Details(Int32 id)
        {
            QueryOptions<ShortUrlInfo> query = new QueryOptions<ShortUrlInfo>() { Where = p => p.ShortUrlId == id };
            ShortUrlInfo? shortUrlInfo = _shortUrlInfoRepo.Get(query);

            return View("ShortUrlInfo", shortUrlInfo);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShortUrl shortUrl)
        {
            String? shortLink;

            try
            {
                shortLink = await _shortenerService.GenerateShortUrlAsync(shortUrl, Request);

                if (String.IsNullOrWhiteSpace(shortLink))
                {
                    ModelState.AddModelError("Url", "Invalid URL");
                    return View(shortUrl);
                }

                if (ModelState.IsValid)
                {
                    ShortUrlInfo shortUrlInfo = new ShortUrlInfo
                    {
                        ShortUrlId = shortUrl.Id,
                        CreatedBy = User.Identity?.Name,
                        CreatedDate = DateTime.UtcNow,

                    };

                    _shortUrlInfoRepo.Insert(shortUrlInfo);
                    await _shortUrlInfoRepo.SaveAsync();

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (DbUpdateException ex)
            {

                ModelState.AddModelError("Url", "Error while saving. This URL may already exist.");
                return View(shortUrl); 
            }

            return View(shortUrl);
        }

        [Authorize]
        public async Task<ActionResult> Delete(Int32 id)
        {
            ShortUrl? shortUrl = await _context.ShortUrls.FindAsync(id);
            if (shortUrl == null)
            {
                return NotFound();
            }
            return View(shortUrl);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Int32 Id)
        {
            ShortUrl? shortUrl = await _context.ShortUrls.FindAsync(Id);

            if (shortUrl == null)
            {
                return NotFound();
            }

            ShortUrlInfo? info = await _context.ShortUrlInfos
                .FirstOrDefaultAsync(x => x.ShortUrlId == Id);

            if (info == null)
            {
                return NotFound();
            }


            bool isAdmin = User.IsInRole("Admin");
            string currentUser = User.Identity.Name ?? "";

            if (!isAdmin && info.CreatedBy != currentUser)
            {
                return Forbid();
            }

            _shortUrlInfoRepo.Delete(info);
            _shortUrlRepo.Delete(shortUrl);

            await _shortUrlInfoRepo.SaveAsync();
            await _shortUrlRepo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/r/{code}")]
        public async Task<ActionResult> RedirectToOriginal(String code)
        {
            var originalUrl = await _shortenerService.GetlUrl(code);
            if (originalUrl == null) return NotFound();
            return Redirect(originalUrl);
        }

        public async Task<String?> ValidateAndGetExistingCode(String url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                return null;
            }

            var existing = await _context.ShortUrls
                .FirstOrDefaultAsync(x => x.Url == url);

            return existing?.Short;
        }


    }
}
