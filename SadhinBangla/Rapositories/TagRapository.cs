using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;
using SadhinBangla.Models.Domain;
using SadhinBangla.Models.ViewModels;

namespace SadhinBangla.Rapositories
{
    public class TagRapository : ITagRapository
    {
        private readonly SadhinBanglaDbContext sadhinBanglaDbContext;

        public TagRapository(SadhinBanglaDbContext sadhinBanglaDbContext)
        {
            this.sadhinBanglaDbContext = sadhinBanglaDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await sadhinBanglaDbContext.tags.AddAsync(tag);
            await sadhinBanglaDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existigTag = await sadhinBanglaDbContext.tags.FindAsync(id);
            if (existigTag != null)
            {
                sadhinBanglaDbContext.tags.Remove(existigTag);
                await sadhinBanglaDbContext.SaveChangesAsync();
                return existigTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return  await sadhinBanglaDbContext.tags.ToListAsync();
            

        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await sadhinBanglaDbContext.tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag =  await sadhinBanglaDbContext.tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await sadhinBanglaDbContext.SaveChangesAsync();
                return existingTag;
            }
            else
            {
                return null;
            }
        }
    }
}
