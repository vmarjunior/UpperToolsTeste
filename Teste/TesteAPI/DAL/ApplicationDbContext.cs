using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Arquivo> Arquivo { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Despesa> Despesa { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";

            builder.Entity<Arquivo>().HasKey(p => p.IdArquivo);
            builder.Entity<Arquivo>().Property(p => p.DataEnvio).IsRequired();
            builder.Entity<Arquivo>().Property(p => p.NomeArquivo).IsRequired();
            builder.Entity<Arquivo>().HasOne(p => p.Cliente).WithMany(p => p.Arquivos).HasForeignKey(p => p.IdCliente).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cliente>().HasKey(p => p.IdCliente);
            builder.Entity<Cliente>().Property(p => p.NomeCliente).IsRequired();

            builder.Entity<Despesa>().HasKey(p => p.IdDespesa);
            builder.Entity<Despesa>().Property(p => p.ValorCobrado).HasColumnType(priceDecimalType);
            builder.Entity<Despesa>().Property(p => p.DataVencimento);
            builder.Entity<Despesa>().Property(p => p.DataPagamento);
            builder.Entity<Despesa>().Property(p => p.TipoDespesa).IsRequired();
            builder.Entity<Despesa>().Property(p => p.ValorMulta).HasColumnType(priceDecimalType);
            builder.Entity<Despesa>().Property(p => p.ValorPago).HasColumnType(priceDecimalType);
            builder.Entity<Despesa>().HasOne(p => p.Arquivo).WithMany(p => p.Despesas).HasForeignKey(p => p.IdArquivo).IsRequired().OnDelete(DeleteBehavior.Cascade);

            SeedData(ref builder);
        }

        protected void SeedData(ref ModelBuilder builder)
        {
            builder.Entity<Cliente>().HasData(new Cliente { IdCliente = 1, NomeCliente = "Auto Escola ABC (TESTE)" });
        }


        public override int SaveChanges()
            => base.SaveChanges();

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
            => base.SaveChanges(acceptAllChangesOnSuccess);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => base.SaveChangesAsync(cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
            => base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
