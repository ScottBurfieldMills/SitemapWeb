using Microsoft.AspNetCore.Mvc;
using SitemapWeb.Data;
using SitemapWeb.Filters;
using SitemapWeb.Models;
using SitemapWeb.Entities;
using System.Threading.Tasks;
using SitemapWeb.Jobs;
using Microsoft.AspNetCore.SignalR;
using SitemapWeb.Enums;

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
            var sitemapSubmission = await CreateSitemapSubmission(vm);

            JobManager.ProcessSitemap(sitemapSubmission.Id);

            return Json(sitemapSubmission);
        }

        private async Task<SitemapSubmission> CreateSitemapSubmission(SubmitSitemapViewModel vm)
        {
            var sitemapSubmission = new SitemapSubmission
            {
                Url = vm.SitemapUrl,
                SitemapSubmissionStatusId = (int)SitemapSubmissions.Created
            };

            _db.SitemapSubmissions.Add(sitemapSubmission);

            await _db.SaveChangesAsync();

            return sitemapSubmission;
        }
    }
}
