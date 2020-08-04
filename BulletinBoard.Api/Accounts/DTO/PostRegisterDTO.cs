using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Accounts.DTO
{
    public class PostRegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string TypeUser { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Bio { get; set; }
    }
}
