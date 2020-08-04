using BulletinBoard.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.DAL
{
    public class BulletinBoardDbContext : IdentityDbContext<User>
    {
        public BulletinBoardDbContext() : base("BulletinBoardDb")
        { }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
