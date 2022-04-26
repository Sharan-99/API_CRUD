using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models
{
    public class APICRUD_DBContext:DbContext
    {
        public APICRUD_DBContext(DbContextOptions<APICRUD_DBContext> options):base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
