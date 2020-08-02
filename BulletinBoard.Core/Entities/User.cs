using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class User : IdentityUser
    {
        public TypeUser TypeUser { get; set; }

        public ICollection<Advert> Adverts { get; set; }
    }
}
