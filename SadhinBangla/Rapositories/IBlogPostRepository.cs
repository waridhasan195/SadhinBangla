using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost blogPost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> GetUrlHandleAsync(string urlHandle);

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
