
using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public BlogPostLikeRepository(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await sadhinBanglaDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await sadhinBanglaDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlogForUser(Guid blogPostId)
        {
            return await sadhinBanglaDbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await sadhinBanglaDbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
