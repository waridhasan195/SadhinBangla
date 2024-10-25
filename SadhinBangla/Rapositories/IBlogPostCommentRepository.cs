using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid bloPostId);
    }
}
