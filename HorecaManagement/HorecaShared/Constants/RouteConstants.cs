namespace Horeca.Shared.Constants
{
    public static class RouteConstants
    {
        /// <summary>
        /// Default Uri separator.
        /// </summary>
        private const string Slash = "/";

        public static class AccountConstants
        {
            public const string LoginSuperAdmin = "SuperAdminAccessToken";
            public const string GetUserClaims = "GetUserClaims";
            public const string RefreshToken = "RefreshToken";
            public const string RevokeToken = $"RefreshToken{Slash}revoke";
            public const string Login = "login";
            public const string Register = "register";
            public const string RegisterAdmin = "register-admin";
            public const string UserPermissions = "UserPermissions";
            public const string GetUserByUserName = $"UserPermissions{Slash}";
            public const string GetAllUsers = $"User";
        }
    }
}