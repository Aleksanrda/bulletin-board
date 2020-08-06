using BulletinBoard.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.DAL.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> bulletinBoardDbSet;

        private BulletinBoardDbContext bulletinBoardRepositoryContext;
        
        public RepositoryBase(BulletinBoardDbContext bulletinBoardDbContext)
        {
            this.bulletinBoardRepositoryContext = bulletinBoardDbContext;
            this.bulletinBoardDbSet = bulletinBoardDbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return bulletinBoardDbSet;
        }

        public IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return bulletinBoardDbSet.Where(expression);
        }

        public void Create(TEntity entity)
        {
            bulletinBoardDbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            bulletinBoardDbSet.Attach(entity);
            bulletinBoardRepositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (bulletinBoardRepositoryContext.Entry(entity).State == EntityState.Detached)
            {
                bulletinBoardDbSet.Attach(entity);
            }

            bulletinBoardDbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = bulletinBoardDbSet.Find(id);
            Delete(entityToDelete);
        }

        public async Task<TEntity> GetByID(object id)
        {
            return await bulletinBoardDbSet.FindAsync(id);
        }
    }
}
