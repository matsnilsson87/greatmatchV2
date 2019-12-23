using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace gr8Match.Models
{
    public class Gr8DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public Gr8DbContext() : base ("Gr8db")
        {
                
        }

    }
}