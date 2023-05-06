using Master.Shared;

namespace Master.Domain.Authorization
{
    /// <summary>
    /// Static permissions of the system are defined here, and they will be added as claims (of type <see cref="Constants.PermissionsClaimType"/>)
    /// to the corresponding roles. Static role permissions are defined in <see cref="StaticRolePermissions"/>.
    /// </summary>
    public static class Permissions
    {
        private const string PermissionsPrefix = Constants.PermissionsClaimType + ".";

        public static readonly string[] ListAll =
        {

            Trip.Create,
            Trip.Update,
            Trip.View,
            Trip.Delete,

        };

        #region Service Permissions
        public static class Trip
        {
            public const string View = PermissionsPrefix + "Trip.View";
            public const string Create = PermissionsPrefix + "Trip.Create";
            public const string Update = PermissionsPrefix + "Trip.Update";
            public const string Delete = PermissionsPrefix + "Trip.Delete";
            public const string Book = PermissionsPrefix + "Trip.Book";
        }
        #endregion
    }
}
