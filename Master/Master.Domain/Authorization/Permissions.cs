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
            Appointment.Create,
            Appointment.Delete,
            Appointment.Update,
            Appointment.View,
            Appointment.ViewAllAppointments,
            Appointment.ViewCalendarByDate,
            Appointment.AccessTotals,
            Appointment.GetUnlimitedPast,

            Attachment.Create,
            Attachment.View,
            Attachment.Delete,

            Patient.Create,
            Patient.Delete,
            Patient.Update,
            Patient.View,
            Patient.ViewAllPatients,

            Service.Create,
            Service.Update,
            Service.View,
            Service.Delete,

            Product.Create,
            Product.Delete,
            Product.Update,
            Product.View,

            Supplier.Create,
            Supplier.Delete,
            Supplier.Update,
            Supplier.View,

            Manufacturer.Create,
            Manufacturer.Delete,
            Manufacturer.Update,
            Manufacturer.View,

            Order.Create,
            Order.Delete,
            Order.Update,
            Order.View,

            Warehouse.Create,
            Warehouse.Update,
            Warehouse.View,

            Inventory.Create,
            Inventory.View,
            Inventory.Update,
            Inventory.Delete,

            Statistics.View,

            Employee.Create,
            Employee.Update,
            Employee.UpdateUserDetails,
            Employee.View,
            Employee.Delete,

            Roles.Create,
            Roles.Delete,
            Roles.Update,
            Roles.View,
            Roles.ViewPermissions,

            RegisteredAbsence.Create,
            RegisteredAbsence.View,
            RegisteredAbsence.Update,
            RegisteredAbsence.Delete,

            Attendance.View,
            Attendance.Update,
            Attendance.ViewCalendar,

            Stream.View,
            Stream.Create,
            Stream.Update,
            Stream.Delete,

            EmailTemplate.View,
            EmailTemplate.Create,
            EmailTemplate.Update,
            EmailTemplate.Delete,

            Notification.Create,

            LegalDocument.Create,
            LegalDocument.View,
            LegalDocument.Delete,
            LegalDocument.Update,

            Settings.View,
            Settings.Update,

            PatientNote.Create,
            PatientNote.View,

            MedicalRecord.Create,
            MedicalRecord.View,
            MedicalRecord.ViewNurseData,
            MedicalRecord.ViewDoctorData,
            MedicalRecord.Update,

            MedicalAttachment.Create,
            MedicalAttachment.View,
            MedicalAttachment.Delete,

            Stock.Create,
            Stock.View,
            Stock.Delete,

            Invoice.Delete,
            Invoice.View,
            Invoice.Create,
            Invoice.Update,

            AppStatistics.ApplicationDashboard,

            Channel.Create,
            Channel.Delete

        };

        #region Appointment Permissions
        public static class Appointment
        {
            public const string View = PermissionsPrefix + "Appointment.View";
            public const string ViewAllAppointments = PermissionsPrefix + "Appointment.ViewOtherDoctorsAppointments";
            public const string Create = PermissionsPrefix + "Appointment.Create";
            public const string Update = PermissionsPrefix + "Appointment.Update";
            public const string Delete = PermissionsPrefix + "Appointment.Delete";

            public const string ViewCalendarByDate = PermissionsPrefix + "Appointment.ViewCalendarByDate";

            public const string AccessTotals = PermissionsPrefix + "Appointment.AccessTotals";

            //Get appointments past the limit set in settings (1 week default)
            public const string GetUnlimitedPast = PermissionsPrefix + "Appointment.GetUnlimitedPast";

        }
        #endregion

        #region Patient Permissions
        public static class Patient
        {
            public const string View = PermissionsPrefix + "Patient.View";
            public const string ViewAllPatients = PermissionsPrefix + "Patient.ViewOtherDoctorsPatients";
            public const string Create = PermissionsPrefix + "Patient.Create";
            public const string Update = PermissionsPrefix + "Patient.Update";
            public const string Delete = PermissionsPrefix + "Patient.Delete";
        }
        #endregion

        #region Attachment Permissions
        public static class Attachment
        {
            public const string Create = PermissionsPrefix + "Attachment.Create";
            public const string View = PermissionsPrefix + "Attachment.View";
            public const string Delete = PermissionsPrefix + "Attachment.Delete";
        }
        #endregion

        #region Product Permissions
        public static class Product
        {
            public const string View = PermissionsPrefix + "Product.View";
            public const string Create = PermissionsPrefix + "Product.Create";
            public const string Update = PermissionsPrefix + "Product.Update";
            public const string Delete = PermissionsPrefix + "Product.Delete";
        }
        #endregion

        #region Service Permissions
        public static class Service
        {
            public const string View = PermissionsPrefix + "Service.View";
            public const string Create = PermissionsPrefix + "Service.Create";
            public const string Update = PermissionsPrefix + "Service.Update";
            public const string Delete = PermissionsPrefix + "Service.Delete";
        }
        #endregion

        #region Employee Permissions
        public static class Employee
        {
            public const string View = PermissionsPrefix + "Employee.View";
            public const string Create = PermissionsPrefix + "Employee.Create";
            public const string Update = PermissionsPrefix + "Employee.Update";
            public const string UpdateUserDetails = PermissionsPrefix + "Employee.UpdateUserDetails";
            public const string Delete = PermissionsPrefix + "Employee.Delete";
        }
        #endregion

        #region Supplier Permissions
        public static class Supplier
        {
            public const string View = PermissionsPrefix + "Supplier.View";
            public const string Create = PermissionsPrefix + "Supplier.Create";
            public const string Update = PermissionsPrefix + "Supplier.Update";
            public const string Delete = PermissionsPrefix + "Supplier.Delete";
        }
        #endregion

        #region Manufacturer Permissions
        public static class Manufacturer
        {
            public const string View = PermissionsPrefix + "Manufacturer.View";
            public const string Create = PermissionsPrefix + "Manufacturer.Create";
            public const string Update = PermissionsPrefix + "Manufacturer.Update";
            public const string Delete = PermissionsPrefix + "Manufacturer.Delete";
        }
        #endregion

        #region Order Permissions
        public static class Order
        {
            public const string View = PermissionsPrefix + "Order.View";
            public const string Create = PermissionsPrefix + "Order.Create";
            public const string Update = PermissionsPrefix + "Order.Update";
            public const string Delete = PermissionsPrefix + "Order.Delete";
        }
        #endregion

        #region Warehouse Permissions
        public static class Warehouse
        {
            public const string View = PermissionsPrefix + "Warehouse.View";
            public const string Create = PermissionsPrefix + "Warehouse.Create";
            public const string Update = PermissionsPrefix + "Warehouse.Update";
        }
        #endregion

        #region Statistics Permissions
        public static class Statistics
        {
            public const string View = PermissionsPrefix + "Statistics.View";
        }
        #endregion

        #region AppStatistics Permissions
        public static class AppStatistics
        {
            public const string ApplicationDashboard = PermissionsPrefix + "Application.Dashboard";
        }
        #endregion

        #region Roles Permissions
        public static class Roles
        {
            public const string View = PermissionsPrefix + "Roles.View";
            public const string ViewPermissions = PermissionsPrefix + "Roles.ViewPermissions";
            public const string Create = PermissionsPrefix + "Roles.Create";
            public const string Update = PermissionsPrefix + "Roles.Update";
            public const string Delete = PermissionsPrefix + "Roles.Delete";
        }
        #endregion

        #region Inventory Permissions
        public static class Inventory
        {
            public const string View = PermissionsPrefix + "Inventory.View";
            public const string Create = PermissionsPrefix + "Inventory.Create";
            public const string Update = PermissionsPrefix + "Inventory.Update";
            public const string Delete = PermissionsPrefix + "Inventory.Delete";
        }
        #endregion

        #region Registered Absence
        public static class RegisteredAbsence
        {
            public const string View = PermissionsPrefix + "RegisteredAbsence.View";
            public const string Create = PermissionsPrefix + "RegisteredAbsence.Create";
            public const string Update = PermissionsPrefix + "RegisteredAbsence.Update";
            public const string Delete = PermissionsPrefix + "RegisteredAbsence.Delete";
        }
        #endregion

        #region Attendance
        public static class Attendance
        {
            public const string View = PermissionsPrefix + "Attendance.View";
            public const string Update = PermissionsPrefix + "Attendance.Update";
            public const string ViewCalendar = PermissionsPrefix + "Attendance.ViewCalendar";
        }
        #endregion

        #region Stream Permissions
        public static class Stream
        {
            public const string View = PermissionsPrefix + "Stream.View";
            public const string Create = PermissionsPrefix + "Stream.Create";
            public const string Update = PermissionsPrefix + "Stream.Update";
            public const string Delete = PermissionsPrefix + "Stream.Delete";
        }
        #endregion

        #region EmailTemplate Permissions
        public static class EmailTemplate
        {
            public const string View = PermissionsPrefix + "EmailTemplate.View";
            public const string Create = PermissionsPrefix + "EmailTemplate.Create";
            public const string Update = PermissionsPrefix + "EmailTemplate.Update";
            public const string Delete = PermissionsPrefix + "EmailTemplate.Delete";
        }
        #endregion

        #region Notification
        public static class Notification
        {
            public const string Create = PermissionsPrefix + "Notification.Create";
        }
        #endregion

        #region Settings
        public static class Settings
        {
            public const string View = PermissionsPrefix + "Settings.View";
            public const string Update = PermissionsPrefix + "Settings.Update";
        }
        #endregion

        #region LegalDocument
        public static class LegalDocument
        {
            public const string View = PermissionsPrefix + "LegalDocument.View";
            public const string Create = PermissionsPrefix + "LegalDocument.Create";
            public const string Update = PermissionsPrefix + "LegalDocument.Update";
            public const string Delete = PermissionsPrefix + "LegalDocument.Delete";
        }
        #endregion

        #region Patient Note Permissions
        public static class PatientNote
        {
            public const string View = PermissionsPrefix + "PatientNote.View";
            public const string Create = PermissionsPrefix + "PatientNote.Create";
        }
        #endregion

        #region Patient Note Permissions
        public static class Stock
        {
            public const string View = PermissionsPrefix + "Stock.View";
            public const string Create = PermissionsPrefix + "Stock.Create";
            public const string Delete = PermissionsPrefix + "Stock.Delete";
        }
        #endregion

        #region Medical Record Permissions
        public static class MedicalRecord
        {
            public const string View = PermissionsPrefix + "MedicalRecord.View";
            public const string Create = PermissionsPrefix + "MedicalRecord.Create";
            public const string Update = PermissionsPrefix + "MedicalRecord.Update";
            public const string ViewNurseData = PermissionsPrefix + "MedicalRecord.ViewNurseData";
            public const string ViewDoctorData = PermissionsPrefix + "MedicalRecord.ViewDoctorData";
        }
        #endregion

        #region Medical Attachment Permissions
        public static class MedicalAttachment
        {
            public const string View = PermissionsPrefix + "MedicalAttachment.View";
            public const string Create = PermissionsPrefix + "MedicalAttachment.Create";
            public const string Delete = PermissionsPrefix + "MedicalAttachment.Delete";
        }
        #endregion

        public static class Invoice
        {
            public const string View = PermissionsPrefix + "Invoice.View";
            public const string Create = PermissionsPrefix + "Invoice.Create";
            public const string Delete = PermissionsPrefix + "Invoice.Delete";
            public const string Update = PermissionsPrefix + "Invoice.Update";
        }

        public static class Channel
        {
            public const string Create = PermissionsPrefix + "Channel.Create";
            public const string Delete = PermissionsPrefix + "Channel.Delete";
        }
    }
}
