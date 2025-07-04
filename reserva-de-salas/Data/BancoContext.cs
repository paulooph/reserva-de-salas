﻿using Microsoft.EntityFrameworkCore;
using reserva_de_salas.Models;

namespace reserva_de_salas.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; } // Adiciona o DbSet para Sala

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
