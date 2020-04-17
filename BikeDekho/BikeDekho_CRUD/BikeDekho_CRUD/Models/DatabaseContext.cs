using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDekho_CRUD.Models
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bike> Bikes { get; set; }
    }
}
