namespace Master.Shared.Enums
{
    public enum AttachmentType : byte
    {
        Image = 1,
        Pdf = 2,
        Doc = 3,
        Docx = 4
    }

    public enum AttachmentRefType : byte
    {
        Appointment = 1,
        Product = 2,
        Patient = 3,
        ProductMovement = 4,
        Absence = 5,
        Contract = 6,
        LegalDocument = 7,
        EmailTemplate = 8
    }
}
