
using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;

namespace SadhinBangla.Rapositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public BlogPostLikeRepository(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
        }
        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await sadhinBanglaDbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
