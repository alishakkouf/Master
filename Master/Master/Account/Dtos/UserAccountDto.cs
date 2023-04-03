using Master.Shared.Enums;

namespace Master.Account.Dtos
{
    public class UserAccountDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        /// <summary>
        /// Role of the user which should be only one currently
        /// </summary>
        public string Role { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
