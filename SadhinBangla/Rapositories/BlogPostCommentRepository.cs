using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public BlogPostCommentRepository(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await sadhinBanglaDbContext.BlogPostComment.AddRangeAsync(blogPostComment);
            await sadhinBanglaDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        async Task<IEnumerable<BlogPostComment>> IBlogPostCommentRepository.GetCommentsByBlogIdAsync(Guid bloPostId)
        {
            return await sadhinBanglaDbContext.BlogPostComment.Where(x => x.BlogPostId == bloPostId).ToListAsync();
        }
    }
}
