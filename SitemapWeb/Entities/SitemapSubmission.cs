using System.ComponentModel.DataAnnotations;

namespace SitemapWeb.Entities
{
    public class SitemapSubmission
    {
        public int Id { get; set; }

        [MaxLength(512)]
        public string Url { get; set; }
    }
}
