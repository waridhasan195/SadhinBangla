using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SadhinBangla.Data;

namespace SadhinBangla.Rapositories
{

    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var superAdminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@sadhinbangla.com");

            if (superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }
            return users;

        }
    }
}
