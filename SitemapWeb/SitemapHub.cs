using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitemapWeb
{
    public class SitemapHub : Hub
    {
        public async Task WatchSubmission(int sitemapSubmissionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sitemapSubmissionId.ToString());
        }
    }
}
