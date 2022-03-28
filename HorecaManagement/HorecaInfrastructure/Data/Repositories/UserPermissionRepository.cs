using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        private readonly DatabaseContext context;

        public UserPermissionRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public List<UserPermission> GetAllUserPermissionsByUserId(string userId)
        {
            return context.UserPermissions.Where(x => x.UserId.Equals(userId)).ToList();
        }
    }
}