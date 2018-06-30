using Microsoft.AspNetCore.Mvc;
using SitemapWeb.Data;
using SitemapWeb.Filters;
using SitemapWeb.Models;
using SitemapWeb.Entities;
using System.Threading.Tasks;

namespace SitemapWeb.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SitemapController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SubmitSitemapViewModel vm)
        {
            var sitemapSubmission = new SitemapSubmission
            {
                Url = vm.SitemapUrl
            };

            _db.SitemapSubmissions.Add(sitemapSubmission);

            await _db.SaveChangesAsync();

            return Json(sitemapSubmission);
        }
    }
}
