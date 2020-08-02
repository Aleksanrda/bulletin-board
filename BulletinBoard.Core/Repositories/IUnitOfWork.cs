using BulletinBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Advert> Adverts { get; }

        IRepository<Comment> Comments { get; }

        Task SaveChangesAsync();
    }
}
