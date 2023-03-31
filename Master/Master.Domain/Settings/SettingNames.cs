using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Settings
{
    public static class SettingNames
    {
        public const string AppTimeZone = "App.TimeZone";
        public const string AppSendOrderFromEmail = "App.SendOrderFromEmail";
        public const string AppCountryPhoneCode = "App.CountryPhoneCode";
        public const string AppLanguage = "App.Language";
        public const string AppSmsLanguage = "App.SmsLanguage";
        public const string AppSmsAppointmentTemplate = "App.SmsAppointmentTemplate";
        public const string AppSmsFromPhone = "App.SmsFromPhone";
        /// <summary>
        /// Set whether the sms functionality enabled or not
        /// </summary>
        public const string AppSmsEnabled = "App.SmsEnabled";
        /// <summary>
        /// Time of day (HH:mm) to send sms to clients 
        /// </summary>
        public const string AppSmsSendTimeOfDay = "App.SmsSendTimeOfDay";
        /// <summary>
        /// Time of day (HH:mm) to commit attendance and check employees out. 
        /// </summary>
        public const string AppCommitAttendanceTimeOfDay = "App.CommitAttendanceTimeOfDay";
        /// <summary>
        /// The hour to handle finished appointments and generate product movements and modify warehouse quantities.
        /// </summary>
        public const string AppGenerateAppointmentMovementsHour = "App.GenerateAppointmentMovementsHour";
        /// <summary>
        /// Number of days before appointment to send SMS to patient.
        /// </summary>
        public const string AppAppointmentReminderDaysCount = "App.AppointmentReminderDaysCount";
        /// <summary>
        /// Limit users without permission 'Appointment.GetUnlimitedPast' to access only 'AppointmentPastLimit' in the past (7 days).
        /// </summary>
        public const string AppAppointmentPastLimit = "App.AppointmentPastLimit";

        public const string AppSendEmailNotificationEnabled = "App.SendEmailNotificationEnabled";

        public const string ClinicName = "Clinic.Name";

        /// <summary>
        /// Used by user name to be 'user@domain-name'
        /// </summary>
        public const string ClinicDomainName = "Clinic.DomainName";

        /// <summary>
        /// Serialized address as JsonAddress
        /// </summary>
        public const string ClinicAddressInfo = "Clinic.AddressInfo";
        public const string ClinicOrderPayeeInfo = "Clinic.OrderPayeeInfo";
        public const string ClinicOrderTemplateFooter = "Clinic.OrderTemplateFooter";
        public const string ClinicOrderEmailBody = "Clinic.OrderEmailBody";
        public const string ClinicLogoBase64 = "Clinic.LogoBase64";
        public const string ClinicTaxRate = "Clinic.TaxRate";
        public const string ClinicRatingEmailSubject = "Clinic.RatingEmailSubject";
        public const string ClinicRatingEmailBody = "Clinic.RatingEmailBody";
        public const string AppVipSwitch = "App.VipSwitch";

        public const string ClinicNpsEmailFrom = "Clinic.NpsEmailFrom";
        public const string ClinicNpsEmailEnabled = "Clinic.NpsEmailEnabled";

        public const string ClinicRecurrentServiceReminder = "Clinic.RecurrentServiceReminder";
        public const string ClinicOnlineAppointmentReservation = "Clinic.OnlineAppointmentReservation";

        public const string ClinicGermanInvoiceSystem = "Clinic.GermanInvoiceSystem";
        public const string ClinicWithTax = "Clinic.WithTax";
        public const string UAEMedicalRecord = "Clinic.UAEMedicalRecord";
        public const string ClinicSpeciality = "Clinic.Speciality";
        public const string InvoicePaymentPeriod = "Clinic.InvoicePaymentPeriod";

        public const string BIC = "Clinic.BIC";
        public const string Bankholder = "Clinic.Bankholder";
        public const string ClinicOwner = "Clinic.Owner";
        public const string ClinicType = "Clinic.Type";
        public const string Country = "Clinic.Country";
        public const string IBAN = "Clinic.IBAN";
        public const string TaxNr = "Clinic.Tax_Nr";
        public const string ClinicEmail = "Clinic.Email";
        public const string ClinicPhone = "Clinic.Phone";
        public const string ClinicCurrency = "Clinic.Currency";
        public const string ClinicOpeningTime = "Clinic.OpeningTime";
        public const string ClinicClosingTime = "Clinic.ClosingTime";
        public const string ClinicWeekStartDay = "Clinic.WeekStartDay";

        public const string MaxAppointmentsCountToBeCreated = "Clinic.MaxAppointmentsCountToBeCreated";
        public const string PaperSize = "Clinic.PaperSize";
        public const string DefaultEmailTemplate = "Clinic.DefaultEmailTemplate";

        public const string WhatsAppEnabled = "WhatsApp.Enabled";
        public const string WhatsAppPhoneNumberId = "WhatsApp.PhoneNumberId";
        public const string WhatsAppAccountId = "WhatsApp.AccountId";
        public const string WhatsAppId = "WhatsApp.Id";
        public const string WhatsAppAccessToken = "WhatsApp.WhatsAppAccessToken";

    }

    public static class SettingDefaults
    {
        public static readonly Dictionary<string, string> Defaults =
            new Dictionary<string, string>
            {
                { SettingNames.AppTimeZone, "Europe/Berlin" },
                { SettingNames.AppSendOrderFromEmail, "dev@yolo.clinic" },
                { SettingNames.AppCountryPhoneCode, "+49" },
                { SettingNames.AppLanguage, SupportedLanguages.English },
                { SettingNames.AppSmsLanguage, SupportedLanguages.German },
                { SettingNames.AppSmsAppointmentTemplate, "Sehr geehrte(r) Frau/Herr {0} gerne erinnern wir Sie an ihren Termin am {1} um {2} Uhr. Wenn sie den Termin nicht wahrnehmen können, bitte ich sie es uns telefonisch mitzuteilen. Wir freuen uns auf ihren Besuch. Vielen Dank, ihr Praxisteam Madri." },
                { SettingNames.AppSmsFromPhone, "KrstinMadri" },
                { SettingNames.AppSmsEnabled, "true" },
                { SettingNames.AppSmsSendTimeOfDay, "08:00" },
                { SettingNames.AppCommitAttendanceTimeOfDay, "23:00" },
                { SettingNames.AppGenerateAppointmentMovementsHour, "23:00" },
                { SettingNames.AppAppointmentReminderDaysCount, "3" },
                { SettingNames.AppAppointmentPastLimit, "7" },
                { SettingNames.AppVipSwitch, "false" },
                { SettingNames.AppSendEmailNotificationEnabled, "false" },

                { SettingNames.ClinicTaxRate, "0.19" },

                { SettingNames.ClinicName, "YOLO" },
                { SettingNames.ClinicDomainName, "yolo.clinic" },
                { SettingNames.ClinicAddressInfo, "" },
                { SettingNames.ClinicOrderPayeeInfo, "YOLO Order Payee" },
                { SettingNames.ClinicOrderTemplateFooter, "YOLO Order Footer" },
                { SettingNames.ClinicRatingEmailSubject, "{0} - Rate Us" },
                { SettingNames.ClinicRatingEmailBody, "Dear Mr./Mrs. {0},\n<br><br>We are happy to get your feedback about our services. Please follow the link: {1}.\n<br><br>Thank you.\n<br>{2}\n<br>" },
                { SettingNames.ClinicLogoBase64,
                    "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTk0IiBoZWlnaHQ9IjMzIiB2aWV3Qm94PSIwIDAgMTk0IDMzIiBmaWxsPSJub25lIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPgo8cGF0aCBkPSJNMTI3LjIyIDI3LjExMjdDMTI1Ljc0IDI3LjExMjcgMTI0LjU0MyAyNS45NiAxMjQuNTQzIDI0LjUzNTdWMC43NjYxMTNIMT"+
                    "ExLjIyNUwxMTEuMjc2IDI3Ljc0NjdDMTExLjI4NCAzMC4yMjQ5IDExMy4zNzIgMzIuMjMzOCAxMTUuOTQ2IDMyLjIzMzhIMTMwLjY1TDEyOS44NjMgMjcuMTEyN0gxMjcuMjJaIiBmaWxsPSIjMTAxODIwIi8+CjxwYXRoIGQ9Ik0xNzkuMTA4IDIuNzE2OEgxNDQuMjQzQzEzNi4wMjMgMi43MTY4IDEyOS4zNTEgOS4xMzA1NCAxMjkuMzUxIDE3LjA1MU"+
                    "MxMjkuMzUxIDI0Ljk2MzIgMTM2LjAxNCAzMS4zODUyIDE0NC4yNDMgMzEuMzg1MkgxNzkuMTA4QzE4Ny4zMjggMzEuMzg1MiAxOTQgMjQuOTcxNCAxOTQgMTcuMDUxQzE5NCA5LjEzODc4IDE4Ny4zMzcgMi43MTY4IDE3OS4xMDggMi43MTY4Wk0xNzkuMDQ4IDE3LjA1MUMxNzkuMDQ4IDIxLjI5OTQgMTc1LjQ3MyAyNC43NDA5IDE3MS4wNTkgMjQuNz"+
                    "QwOUgxNTAuNzE4QzE0Ni4zMDQgMjQuNzQwOSAxNDIuNzI5IDIxLjI5OTQgMTQyLjcyOSAxNy4wNTFDMTQyLjcyOSAxMi44MDI2IDE0Ni4zMDQgOS4zNjEwOCAxNTAuNzE4IDkuMzYxMDhIMTcxLjA1OUMxNzUuNDY0IDkuMzYxMDggMTc5LjA0OCAxMi44MDI2IDE3OS4wNDggMTcuMDUxWiIgZmlsbD0iIzEwMTgyMCIvPgo8cGF0aCBkPSJNOTIuMTk0ID"+
                    "IuNzc0NDFINjIuNjU3OEM1NC40Mzc2IDIuNzc0NDEgNDcuNzY1NiA5LjE4ODE2IDQ3Ljc2NTYgMTcuMTA4NkM0Ny43NjU2IDI1LjAyMDggNTQuNDI5IDMxLjQ0MjggNjIuNjU3OCAzMS40NDI4SDkyLjE5NEMxMDAuNDE0IDMxLjQ0MjggMTA3LjA4NiAyNS4wMjkgMTA3LjA4NiAxNy4xMDg2QzEwNy4wODYgOS4xOTYzOSAxMDAuNDE0IDIuNzc0NDEgOT"+
                    "IuMTk0IDIuNzc0NDFaTTkyLjkyMTEgMTcuMTA4NkM5Mi45MjExIDIxLjM1NyA4OS4zNDU2IDI0Ljc5ODUgODQuOTMxOSAyNC43OTg1SDY5LjkxOTlDNjUuNTA2MiAyNC43OTg1IDYxLjkzMDcgMjEuMzU3IDYxLjkzMDcgMTcuMTA4NkM2MS45MzA3IDEyLjg2MDIgNjUuNTA2MiA5LjQxODY5IDY5LjkxOTkgOS40MTg2OUg4NC45MzE5Qzg5LjM0NTYgOS"+
                    "40MTg2OSA5Mi45MjExIDEyLjg2MDIgOTIuOTIxMSAxNy4xMDg2WiIgZmlsbD0iIzEwMTgyMCIvPgo8cGF0aCBkPSJNMTYuOTM2NSAxNi42OTc3SDI1LjkxOEMyNS45MTggMTYuNjk3NyAxMy43MTE3IDAuOTU1NTY2IDEzLjc4MDIgMC45NTU1NjZIMEw5LjcgMTMuNTI3OEMxMS4zMDgxIDE1LjQ5NTYgMTQuMzM2MiAxNi42OTc3IDE2LjkzNjUgMTYuNj"+
                    "k3N1oiIGZpbGw9IiMxMDE4MjAiLz4KPHBhdGggZD0iTTM1LjA4OSAwLjk4MDIwNUwyMS41MjI2IDI0LjY2NzRDMjEuNDcxMyAyNC43NDk4IDIxLjQyIDI0LjgzMjEgMjEuMzYwMSAyNC45MDYyQzIwLjcxODYgMjUuNjg4NCAxOS43NDM0IDI2LjE0MTIgMTguNjk5OSAyNi4xNDEySDQuOTUzOTRDNC45NTM5NCAyNi4xNDEyIDQuMTE1NjcgMzEuMjk1Mi"+
                    "A0LjA1NTc5IDMxLjIzNzZDMy45OTU5MiAzMS4xOCAyNS4wMzgyIDMxLjIzNzYgMjUuMDM4MiAzMS4yMzc2QzI5LjE4NjggMzEuMjM3NiAzMy42MzQ4IDI5Ljc2MzggMzUuNjg3NyAyNi4yOTc2QzUwLjQ4NTggMS4zMzQyNCA1MC44NTM2IDEuMDM3ODQgNTAuODUzNiAwLjk4MDIwNUM1MC44NTM2IDAuOTIyNTcxIDM1LjA4OSAwLjk4MDIwNSAzNS4wOD"+
                    "kgMC45ODAyMDVaIiBmaWxsPSIjMTAxODIwIi8+Cjwvc3ZnPgo=" },
                { SettingNames.ClinicNpsEmailFrom, "dev@yolo.clinic" },
                { SettingNames.ClinicNpsEmailEnabled, "false" },
                { SettingNames.BIC, "" },
                { SettingNames.Bankholder, "" },
                { SettingNames.ClinicOwner, "" },
                { SettingNames.ClinicType, "" },
                { SettingNames.Country, "" },
                { SettingNames.IBAN, "" },
                { SettingNames.TaxNr, "" },
                { SettingNames.ClinicEmail, "" },
                { SettingNames.ClinicPhone, "" },
                { SettingNames.ClinicCurrency, "" },
                { SettingNames.ClinicOpeningTime, "08:00" },
                { SettingNames.ClinicClosingTime, "22:00" },
                { SettingNames.ClinicWeekStartDay, "Monday" },
                { SettingNames.MaxAppointmentsCountToBeCreated, "4" },
                { SettingNames.PaperSize, "A5" },
                { SettingNames.ClinicRecurrentServiceReminder, "15" },
                { SettingNames.ClinicOnlineAppointmentReservation, "false" },
                { SettingNames.ClinicGermanInvoiceSystem, "false" },
                { SettingNames.ClinicWithTax, "false" },
                { SettingNames.UAEMedicalRecord, "false" },
                { SettingNames.ClinicSpeciality, "Aesthetics " },
                { SettingNames.ClinicOrderEmailBody, "Guten Tag, Ich hoffe Ihnen geht es gut! Anbei erhalten Sie eine Liste mit Produkten, die wir von Ihnen bestellen wollen. Falls Sie weitere Fragen zur der Bestellung haben stehen wir Ihnen gerne zur Verfügung. Mit freundlichen Grüßen Praxisteam Krstin Madri" },
                { SettingNames.DefaultEmailTemplate, "<!DOCTYPE html>\r\n<html>\r\n\t<head>\r\n\r\n\t\t<title>Welcome to our clinic</title>\r\n\t</head>\r\n\t<body style=\"background-color:#F5F6F8;font-family: Arial, sans-serif;box-sizing:border-box;font-size:16px;\">\r\n\t\t<div style=\"background-color:#fff;margin:30px;box-sizing:border-box;font-size:16px;\">\r\n\t\t\t<h1 style=\"padding:40px;box-sizing:border-box;font-size:24px;color:#000000;background-color:#cb5f51;margin:0;\">Clinic Name</h1>\r\n\r\n\t\t\t<div style=\"box-sizing:border-box;padding:0 40px 20px;\">\r\n<p>\r\n\r\nWelcome to our clinic!.\r\nYOLO Team\r\n</p>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</body>\r\n</html>"},

                { SettingNames.WhatsAppEnabled, "false" },
                { SettingNames.WhatsAppPhoneNumberId, "" },
                { SettingNames.WhatsAppAccountId, "" },
                { SettingNames.WhatsAppId, "" },
                { SettingNames.WhatsAppAccessToken, "" },
                { SettingNames.InvoicePaymentPeriod, "15" },

            };
    }
}
