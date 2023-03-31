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
                    StaticRoleNames.Doctor,
                    new []
                    {
                        Permissions.Appointment.View,
                        Permissions.Appointment.Create,
                        Permissions.Appointment.Update,
                        Permissions.Appointment.Delete,
                        Permissions.Appointment.ViewCalendarByDate,

                        Permissions.Patient.View,
                        Permissions.Patient.Create,
                        Permissions.Patient.Update,

                        Permissions.Product.View,
                        Permissions.Product.Create,
                        Permissions.Product.Update,

                        Permissions.Service.View,
                        Permissions.Service.Create,
                        Permissions.Service.Update,

                        Permissions.Attachment.Create,
                        Permissions.Attachment.Delete,

                        Permissions.Employee.View
                    }
                },
                {
                    StaticRoleNames.Patient,
                    new string[0]
                },
            };
    }
}