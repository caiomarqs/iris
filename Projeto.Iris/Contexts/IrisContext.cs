using Microsoft.EntityFrameworkCore;
using Projeto.Iris.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.Contexts
{
    public class IrisContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public IrisContext(DbContextOptions options) : base(options) { }

    }
}
