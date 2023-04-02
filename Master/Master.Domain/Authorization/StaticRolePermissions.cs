using System.Collections.Generic;

namespace Master.Domain.Authorization
{
    /// <summary>
    /// Static roles and permissions for them are defined here.
    /// System will seed them on startup.
    /// </summary>
    public static class StaticRolePermissions
    {
        public static readonly Dictionary<string, string[]> Roles =
            new Dictionary<string, string[]>
            {
                {
                    StaticRoleNames.Administrator,
                    Permissions.ListAll
                },
                {
                    StaticRoleNames.Passenger,
                    new []
                    {
                        Permissions.Appointment.View,

                    }
                }
                //{
                //    StaticRoleNames.,
                //    new string[0]
                //},
            };
    }
}