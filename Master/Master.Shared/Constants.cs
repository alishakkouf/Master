using System;

namespace Master.Shared
{
    public static class Constants
    {


        //public const string EmailRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.)+(\w){2,}$";
        public const string EmailRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.?)+(\w){2,}$";
        public const string PhoneCountryCodeExpression = @"^(\+)[1-9]\d{0,3}$";
        public const string PhoneRegularExpression = @"^(0|00|\+)[1-9]\d{5,14}$";
        public const string UserNameRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.)*[\w\-]{2,}$";
        public const string HexColorRegularExpression = @"^#([a-fA-F0-9]{3}){1,2}$";
        public const string EnglishCharactersRegex = @"^[a-zA-Z]*$";
        public const string CharactersRegularExpression = @"^[a-zA-Z \u00E4\u00F6\u00FC\u00C4\u00D6\u00DC\u00df]*$";
        public const string ArabicOrEnglishOrGermanCharactersRegex = @"^[a-zA-Z \u00E4\u00F6\u00FC\u00C4\u00D6\u00DC\u00df\u0621-\u065f]*$";
        public const string NumbersRegularExpression = @"^[0-9]*$";
        public const string InternationalPhoneRegularExpression = @"^(\+)[1-9]\d{8,14}$";
        public const string UrlRegularExpression = @"^(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";
        public const string VerifyPhoneRegularExpression = @"^\d{6}$";
        /// <summary>
        /// Regular Expression Pattern for Time in format HH:mm
        /// </summary>
        public const string HoursMinutesRegularExpression = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";

        public const string AdministratorUserName = "Administrator";
        public const string DefaultPassword = "P@ssw0rd";
        public const string DefaultPhoneNumber = "0";

        public const string SuperAdminRoleName = "SuperAdmin";
        public const string SuperAdminUserName = "SuperAdmin";
        public const string SuperAdminEmail = "superadmin@yolo-clinic.de";

        /// <summary>
        /// Id of the first seeded tenant
        /// </summary>
        public const int DefaultTenantId = 1;
        public const string DefaultTenantAdmin = "Administrator@yolo-clinic.de";
        public const string DefaultTenantDomain = "yolo-clinic.de";

        public const string UploadsFolderName = "Uploads";
        public const string ImagesFolderName = "Images";
        public const string AttachmentsFolderName = "Attachments";
        public const string ThumbnailsFolderName = "Thumbnails";
        public const string TemplatesFolderName = "Templates";
        public const string FontsFolderName = "Fonts";
        
        public const string GermanyCountryName = "Germany";

        public const string DefaultCategoryName = "Default";

        public const int DefaultPageIndex = 1;
        public const int DefaultPageSize = 10;

        public const decimal DefaultTaxRate = 0.19m;

        // channels
        public const int OtherChannelId = 1;
        public const string OtherChannel = "Other";
        public const string DefaultChannel = "Direct";
        public const string FacebookChannel = "Facebook";
        public const string YoutubeChannel = "Youtube";
        public const string InstagramChannel = "Instagram";
        public const string TwitterChannel = "Twitter";
        public const string GoogleChannel = "Google";
        public const string TiktokChannel = "Tiktok";
        public const string RecommendedChannel = "Recommended";
        /// <summary>
        /// The limit count to be considered as getting all items
        /// </summary>
        public const int AllItemsPageSize = 1000;

        /// <summary>
        /// Default start of week (Monday)
        /// </summary>
        public const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        /// <summary>
        /// The custom claim type for the role permissions
        /// </summary>
        public const string PermissionsClaimType = "Permissions";

        /// <summary>
        /// The custom claim type to insure active user
        /// </summary>
        public const string ActiveUserClaimType = "UserIsActive";

        /// <summary>
        /// Maximum size of profile image, default = 1 mb
        /// </summary>
        public const int MaxProfileImageSize = 1;

        /// <summary>
        /// Maximum size of attachment file, default = 5 mb
        /// </summary>
        public const int MaxAttachmentSize = 20;

        public const string ExcelFileMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string PdfFileMimeType = "application/pdf";
        public const string HtmlFileMimeType = "text/html";

        public const string AppIgnoreTenantIdKey = "true";


        public const string AppAuditedArrayKey = "App:AuditedArray";

    }
}
