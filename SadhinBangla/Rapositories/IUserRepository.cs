using Microsoft.AspNetCore.Identity;

namespace SadhinBangla.Rapositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
