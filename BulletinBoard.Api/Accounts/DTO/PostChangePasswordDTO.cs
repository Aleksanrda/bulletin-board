using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Accounts.DTO
{
    public class PostChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Password must be between 6-40 characters.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
