using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitemapWeb.Entities
{
    public class SitemapSubmission
    {
        public int Id { get; set; }

        [MaxLength(512), Required]
        public string Url { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey("SitemapSubmissionStatusId")]
        public SitemapSubmissionStatus SitemapSubmissionStatus { get; set; }

        public int SitemapSubmissionStatusId { get; set; }
    }
}
