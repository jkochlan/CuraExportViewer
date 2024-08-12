using Migrations.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=MigrationDBString")
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IEntity>().HasKey(e => e.Id);
        }
    }
}
