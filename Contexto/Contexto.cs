using Blazor.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Blazor.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Cobros> Cobros { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source = Data\Ventas.db"); ;

        }


    }

}