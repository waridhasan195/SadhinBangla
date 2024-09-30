using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public BlogPostRepository(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
        }

        public  async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            var blogpost =  await sadhinBanglaDbContext.BlogPosts.AddAsync(blogPost);
            await sadhinBanglaDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await sadhinBanglaDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
