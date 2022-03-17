using MandradeFrameworks.Repositorios.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MandradeFrameworks.Repositorios.Models
{
    public abstract class StandardContext : DbContext
    {
        public StandardContext(DbContextOptions<StandardContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.AplicarModelBuilders();
    }
}
