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

            await Clients.Group(sitemapSubmissionId.ToString()).SendAsync("SubmissionUpdate", new List<string>
            {
                "test",
                "test2",
                "test3"
            });
        }
    }
}
