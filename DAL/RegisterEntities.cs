using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace DAL
//{
//    public partial class RegisterEntities : DbContext
//    {
//        /// <inheritdoc />
//        ///Priority override
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Employer>().Map(m => m.Requires("Deleted").HasValue(false));
//            modelBuilder.Entity<Employee>().Map(m => m.Requires("Deleted").HasValue(false));
//        }
//    }
//}
