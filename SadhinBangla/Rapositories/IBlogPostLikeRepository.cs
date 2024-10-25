using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlogForUser(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);


    }
}
