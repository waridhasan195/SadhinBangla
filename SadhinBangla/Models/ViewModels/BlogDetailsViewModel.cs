using SadhinBangla.Models.Domain;

namespace SadhinBangla.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDaate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Navigation Property
        public ICollection<Tag> Tags { get; set; }

        public int TotalLikes { get; set; }
    }
}
