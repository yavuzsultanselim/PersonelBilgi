using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelBilgi
{
    public class PersonelDb:DbContext
    {
        public PersonelDb()
        {
            Database.Connection.ConnectionString = "server=.;database=PersonelDb;trusted_connection=true";
        }
        public DbSet<Personel> Personeller { get; set; }
    }
}
