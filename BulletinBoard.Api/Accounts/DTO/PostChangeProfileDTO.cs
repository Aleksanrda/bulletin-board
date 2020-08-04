using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Api.Accounts.DTO
{
    public class PostChangeProfileDTO
    {
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Bio { get; set; }
    }
}
