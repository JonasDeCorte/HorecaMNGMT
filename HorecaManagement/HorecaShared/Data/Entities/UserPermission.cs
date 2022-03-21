using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Entities
{
    public class UserPermission : BaseEntity
    {
        private ApplicationUser? _user;
        private Permission? _permission;

        public string UserId { get; set; }

        public ApplicationUser User
        {
            set => _user = value;
            get => _user
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(User));
        }

        public int PermissionId { get; set; }

        public Permission Permission
        {
            set => _permission = value;
            get => _permission
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Permission));
        }
    }
}