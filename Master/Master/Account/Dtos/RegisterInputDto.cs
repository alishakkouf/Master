using System.ComponentModel.DataAnnotations;
using Master.Shared;

namespace Master.Account.Dtos
{
    public class RegisterInputDto
    {
        [Required(ErrorMessage = "Required")]
        [RegularExpression(Constants.ArabicOrEnglishOrGermanCharactersRegex, ErrorMessage = "OnlyCharactersInput")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(Constants.ArabicOrEnglishOrGermanCharactersRegex, ErrorMessage = "OnlyCharactersInput")]
        public string LastName { get; set; }

        /// <summary>
        /// Acceptable phone must start with + and country code then area code then number, 9 to 15 digits in total
        /// </summary>
        [Required(ErrorMessage = "Required")]
        //[RegularExpression(Constants.InternationalPhoneRegularExpression, ErrorMessage = "PhoneNumberInput")]
        public string PhoneNumber { get; set; }

        //[RegularExpression(Constants.PhoneCountryCodeExpression, ErrorMessage = "InvalidCountryPhoneCodeFormat")]
        //public string PhoneCountryCode { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "StringLength{2}")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(Constants.EmailRegularExpression, ErrorMessage = "InvalidEmailFormat")]
        public string Email { get; set; }
    }
}
