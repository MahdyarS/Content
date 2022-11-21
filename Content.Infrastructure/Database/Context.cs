using Content.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Content.Infrastructure.Database
{
    public class Context : DbContext
    {
        private static readonly DbContextOptions<Context> _defaultOptions = new DbContextOptionsBuilder<Context>().UseSqlServer().Options;

        public Context() : base(_defaultOptions)
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var type = typeof(Entity);
            var entities = type.Assembly.GetTypes().Where(T => typeof(Entity).IsAssignableFrom(T));

            foreach (var entity in entities)
            {
                modelBuilder.Entity(entity).ToTable(entity.Name);
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<Entity>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
