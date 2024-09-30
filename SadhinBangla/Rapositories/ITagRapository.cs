using SadhinBangla.Models.Domain;

namespace SadhinBangla.Rapositories
{
    public interface ITagRapository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
