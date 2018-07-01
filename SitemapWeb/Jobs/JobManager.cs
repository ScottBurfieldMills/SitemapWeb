using Hangfire;
using System;
using System.Linq.Expressions;

namespace SitemapWeb.Jobs
{
    public static class JobManager
    {
        public static void ProcessSitemap(int sitemapSubmissionId)
        {
            BackgroundJob.Enqueue<SitemapProcessor>(x => x.Process(sitemapSubmissionId));
        }
    }
}
