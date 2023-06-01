using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : DbContext
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<Entity>();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            int result = dbContext.SaveChanges();
            return result;
        }
        public int Update(Entity entity)
        {
            entities.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            var result = dbContext.SaveChanges();
            return result;
        }

        public int Delete(Key key)
        {
            Entity FindData = entities.Find(key);
            entities.Remove(FindData);
            var result = dbContext.SaveChanges();
            return result;
        }
    }
}
