using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BulletinBoardDbContext _bulletinBoardDbContext;

        private IRepository<User> _users;
        private IRepository<Advert> _adverts;
        private IRepository<Comment> _comments;
        private IRepository<Category> _categories;

        public UnitOfWork(BulletinBoardDbContext bulletinBoardDbContext)
        {
            _bulletinBoardDbContext = bulletinBoardDbContext;
        }

        public IRepository<User> Users
        {
            get
            {
                return _users ??
                    (_users = new RepositoryBase<User>(_bulletinBoardDbContext));
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return _categories ??
                    (_categories = new RepositoryBase<Category>(_bulletinBoardDbContext));
            }
        }

        public IRepository<Advert> Adverts
        {
            get
            {
                return _adverts ??
                    (_adverts = new RepositoryBase<Advert>(_bulletinBoardDbContext));
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return _comments ??
                    (_comments = new RepositoryBase<Comment>(_bulletinBoardDbContext));
            }
        }

        public async Task SaveChangesAsync()
        {
            await _bulletinBoardDbContext.SaveChangesAsync();
        }
    }
}
