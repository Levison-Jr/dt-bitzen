using DTBitzen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DTBitzen.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reserva>()
                .HasIndex(r => new { r.DataInicio, r.HoraInicio })
                .IsUnique();

            modelBuilder.Entity<Reserva>()
                .HasIndex(r => new { r.DataFim, r.HoraFim })
                .IsUnique();
        }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}
