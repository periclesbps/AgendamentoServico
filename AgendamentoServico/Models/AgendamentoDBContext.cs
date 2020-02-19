using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgendamentoServico.Models
{
    public partial class AgendamentoDBContext : DbContext
    {
        public AgendamentoDBContext()
        {
        }

        public AgendamentoDBContext(DbContextOptions<AgendamentoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Itens> Itens { get; set; }
        public virtual DbSet<OrdemServico> OrdemServico { get; set; }
        public virtual DbSet<Tecnico> Tecnico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;initial catalog=AgendamentoDB;integrated security=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.HasKey(e => new { e.Data, e.IdordemServico })
                    .HasName("PK__Agendame__0E947CD8EA28CF17");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.IdordemServico).HasColumnName("IDOrdemServico");

                entity.HasOne(d => d.IdordemServicoNavigation)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.IdordemServico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OREDMSERVICO");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone).HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Telefone2).HasColumnType("decimal(11, 0)");
            });

            modelBuilder.Entity<Itens>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UQ__Itens__008BA9EFB8549BF2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrdemServico>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.Iditem).HasColumnName("IDItem");

                entity.Property(e => e.Idtecnico).HasColumnName("IDTecnico");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.OrdemServico)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE");

                entity.HasOne(d => d.IditemNavigation)
                    .WithMany(p => p.OrdemServico)
                    .HasForeignKey(d => d.Iditem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITEM");

                entity.HasOne(d => d.IdtecnicoNavigation)
                    .WithMany(p => p.OrdemServico)
                    .HasForeignKey(d => d.Idtecnico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECNICO");
            });

            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone).HasColumnType("decimal(11, 0)");

                entity.Property(e => e.Telefone2).HasColumnType("decimal(11, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
