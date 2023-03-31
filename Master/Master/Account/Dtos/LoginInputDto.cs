using Master.Common;
using System.ComponentModel.DataAnnotations;

namespace Master.Account.Dtos
{
    public class LoginInputDto : IShouldNormalize
    {
        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "StringLength{2}")]
        public string Password { get; set; }

        public void Normalize()
        {
            UserName = UserName.Trim();
        }
    }
}
