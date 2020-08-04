using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Role : IdentityRole
    {
        public Role()
        {

        }

        public Role(string roleName, string description) : base(roleName)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
