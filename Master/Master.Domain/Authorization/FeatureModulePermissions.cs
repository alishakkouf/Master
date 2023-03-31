using System.Collections.Generic;
using Master.Domain.Features;

namespace Master.Domain.Authorization
{
    /// <summary>
    /// Feature modules and their required permissions.
    /// </summary>
    public static class FeatureModulePermissions
    {
        public static readonly Dictionary<string, string[]> Modules =
            new Dictionary<string, string[]>
            {
                {
                    FeatureNames.AppointmentModule,
                    new []
                    {
                        Permissions.Appointment.Create,
                        Permissions.Appointment.Delete,
                        Permissions.Appointment.Update,
                        Permissions.Appointment.View,
                        Permissions.Appointment.ViewCalendarByDate,
                        Permissions.Appointment.AccessTotals,
                        Permissions.Appointment.GetUnlimitedPast,

                        Permissions.Attachment.Create,
                        Permissions.Attachment.Delete,
                        
                        Permissions.Patient.Create,
                        Permissions.Patient.Delete,
                        Permissions.Patient.Update,
                        Permissions.Patient.View,

                        Permissions.Product.Create,
                        Permissions.Product.Delete,
                        Permissions.Product.Update,
                        Permissions.Product.View,
                        
                        Permissions.Service.Create,
                        Permissions.Service.Update,
                        Permissions.Service.View,
                        Permissions.Service.Delete,
                    }
                },
                {
                    FeatureNames.ErpModule,
                    new []
                    {
                        Permissions.Product.Create,
                        Permissions.Product.Delete,
                        Permissions.Product.Update,
                        Permissions.Product.View,

                        Permissions.Supplier.Create,
                        Permissions.Supplier.Delete,
                        Permissions.Supplier.Update,
                        Permissions.Supplier.View,

                        Permissions.Manufacturer.Create,
                        Permissions.Manufacturer.Delete,
                        Permissions.Manufacturer.Update,
                        Permissions.Manufacturer.View,

                        Permissions.Order.Create,
                        Permissions.Order.Delete,
                        Permissions.Order.Update,
                        Permissions.Order.View,

                        Permissions.Warehouse.Create,
                        Permissions.Warehouse.Update,
                        Permissions.Warehouse.View,

                        Permissions.Inventory.Create,
                        Permissions.Inventory.View,
                        Permissions.Inventory.Update,

                        Permissions.Statistics.View,
                    }
                },
                {
                    FeatureNames.HRModule,
                    new []
                    {
                        Permissions.Employee.Create,
                        Permissions.Employee.Update,
                        Permissions.Employee.UpdateUserDetails,
                        Permissions.Employee.View,
                        Permissions.Employee.Delete,

                        Permissions.Roles.Create,
                        Permissions.Roles.Delete,
                        Permissions.Roles.Update,
                        Permissions.Roles.View,
                        Permissions.Roles.ViewPermissions,

                        Permissions.RegisteredAbsence.Create,
                        Permissions.RegisteredAbsence.View,
                        Permissions.RegisteredAbsence.Update,

                        Permissions.Attendance.View,
                        Permissions.Attendance.Update,
                        Permissions.Attendance.ViewCalendar,
                    }
                },
                {
                    FeatureNames.CrmModule,
                    new []
                    {
                        Permissions.Stream.View,
                        Permissions.Stream.Create,
                        Permissions.Stream.Update,
                        Permissions.Stream.Delete,

                        Permissions.EmailTemplate.View,
                        Permissions.EmailTemplate.Create,
                        Permissions.EmailTemplate.Update,
                        Permissions.EmailTemplate.Delete,

                        Permissions.Notification.Create,
                    }
                },
            };
    }
}