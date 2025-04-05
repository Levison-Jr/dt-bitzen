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
                .HasIndex(r => new { r.Data, r.HoraInicio, r.SalaId, r.Status })
                .IsUnique();

            modelBuilder.Entity<Reserva>()
                .Property(r => r.HoraInicio)
                .HasColumnType("time");

            modelBuilder.Entity<Reserva>()
                .Property(r => r.HoraFim)
                .HasColumnType("time");
        }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}
