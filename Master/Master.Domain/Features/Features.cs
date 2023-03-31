using System.Collections.Generic;

namespace Master.Domain.Features
{
    public static class FeatureNames
    {
        public static readonly List<string> All = new List<string>
        {
            AppointmentModule,
            ErpModule,
            HRModule,
            CrmModule
        };

        public const string AppointmentModule = "Appointment.Module";

        public const string ErpModule = "Erp.Module";

        public const string HRModule = "HR.Module";

        public const string CrmModule = "Crm.Module";
    }
}
