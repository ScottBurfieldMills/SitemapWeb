using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SitemapWeb.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace SitemapWeb.Jobs
{
    public class SitemapProcessor
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<SitemapHub> _hubContext;

        public SitemapProcessor(ApplicationDbContext db, IHubContext<SitemapHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        [Display(Name = "Process Sitemap - {0}")]
        public void Process(int sitemapSubmissionId)
        {
            var sitemapSubmission = _db.SitemapSubmissions.Single(x => x.Id == sitemapSubmissionId);

            _hubContext.Clients.Groups(sitemapSubmissionId.ToString()).SendAsync("SubmissionUpdate", "from hangfire");
        }
    }
}
