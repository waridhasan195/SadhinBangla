using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await sadhinBanglaDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                sadhinBanglaDbContext.BlogPosts.Remove(existingBlog);
                await sadhinBanglaDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;      
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await sadhinBanglaDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await sadhinBanglaDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await sadhinBanglaDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishDaate = blogPost.PublishDaate;
                existingBlog.Tags = blogPost.Tags;

                await sadhinBanglaDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<BlogPost?> GetUrlHandleAsync(string urlHandle)
        {
            return await sadhinBanglaDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }
    }
}
