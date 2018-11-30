using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SitemapValidator.Core;
using SitemapWeb.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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

            // _hubContext.Clients.Groups(sitemapSubmissionId.ToString()).SendAsync("SubmissionUpdate", "from hangfire");

            var progressUpdater = new SignalRProgressUpdater(_hubContext, sitemapSubmissionId.ToString());

            using (var client = new HttpClient())
            {
                var validator = new SitemapValidator.Core.SitemapValidator(client, progressUpdater);

                var retriever = new SitemapRetriever(client);
                var sitemap = retriever.Retrieve(sitemapSubmission.Url);

                validator.Validate(sitemap, new Options
                {
                    ExpectedStatusCode = 200,
                    Delay = 1000
                });
            }
        }
    }

    public class SignalRProgressUpdater : IProgressUpdater
    {
        private readonly IHubContext<SitemapHub> _hubContext;
        private readonly string _groupId;

        public SignalRProgressUpdater(IHubContext<SitemapHub> hubContext, string groupId)
        {
            _hubContext = hubContext;
            _groupId = groupId;
        }

        public void Log(SitemapValidator.Core.ValidationResult result, bool verbose = false)
        {
            _hubContext.Clients.Groups(_groupId).SendAsync("SubmissionUpdate", result.Url, result.ActualHttpStatusCode);
        }

        public void UpdateStatusText(string message, bool verbose = false)
        {

        }
    }
}
