using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo1212shubenok.Models;

namespace demo1212shubenok.Context;

public partial class User11072Context : DbContext
{
    public User11072Context()
    {
    }

    public User11072Context(DbContextOptions<User11072Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Active> Actives { get; set; }

    public virtual DbSet<DopPr> DopPrs { get; set; }

    public virtual DbSet<Manuf> Manufs { get; set; }

    public virtual DbSet<Prod> Prods { get; set; }

    public virtual DbSet<Prodazha> Prodazhas { get; set; }

    public virtual DbSet<ProdazhaPr> ProdazhaPrs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table1> Table1s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159;Port=5432;Database=user11072;Username=user11072;Password=15206");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Active>(entity =>
        {
            entity.HasKey(e => e.IdAct).HasName("active_pk");

            entity.ToTable("active");

            entity.Property(e => e.IdAct)
                .ValueGeneratedNever()
                .HasColumnName("id_act");
            entity.Property(e => e.NameAct)
                .HasColumnType("character varying")
                .HasColumnName("name_act");
        });

        modelBuilder.Entity<DopPr>(entity =>
        {
            entity.HasKey(e => new { e.IdPr, e.IdDop }).HasName("dop_pr_pk");

            entity.ToTable("dop_pr");

            entity.Property(e => e.IdPr).HasColumnName("id_pr");
            entity.Property(e => e.IdDop).HasColumnName("id_dop");

            entity.HasOne(d => d.IdPrNavigation).WithMany(p => p.DopPrs)
                .HasForeignKey(d => d.IdPr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dop_pr_prods_fk");
        });

        modelBuilder.Entity<Manuf>(entity =>
        {
            entity.HasKey(e => e.IdManuf).HasName("manuf_pk");

            entity.ToTable("manuf");

            entity.Property(e => e.IdManuf)
                .ValueGeneratedNever()
                .HasColumnName("id_manuf");
            entity.Property(e => e.DateNach).HasColumnName("date_nach");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Prod>(entity =>
        {
            entity.HasKey(e => e.IdPr).HasName("prods_pk");

            entity.ToTable("prods");

            entity.Property(e => e.IdPr)
                .ValueGeneratedNever()
                .HasColumnName("id_pr");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Descr)
                .HasColumnType("character varying")
                .HasColumnName("descr");
            entity.Property(e => e.IdAct).HasColumnName("id_act");
            entity.Property(e => e.IdManuf).HasColumnName("id_manuf");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.IdActNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdAct)
                .HasConstraintName("prods_active_fk");

            entity.HasOne(d => d.IdManufNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdManuf)
                .HasConstraintName("prods_manuf_fk");
        });

        modelBuilder.Entity<Prodazha>(entity =>
        {
            entity.HasKey(e => e.IdProdazha).HasName("prodazha_pk");

            entity.ToTable("prodazha");

            entity.Property(e => e.IdProdazha)
                .ValueGeneratedNever()
                .HasColumnName("id_prodazha");
            entity.Property(e => e.Date)
                .HasColumnType("character varying")
                .HasColumnName("date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Prodazhas)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("prodazha_users_fk");
        });

        modelBuilder.Entity<ProdazhaPr>(entity =>
        {
            entity.HasKey(e => new { e.IdProdazha, e.IdPr }).HasName("prodazha_pr_pk");

            entity.ToTable("prodazha_pr");

            entity.Property(e => e.IdProdazha).HasColumnName("id_prodazha");
            entity.Property(e => e.IdPr).HasColumnName("id_pr");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.IdPrNavigation).WithMany(p => p.ProdazhaPrs)
                .HasForeignKey(d => d.IdPr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prodazha_pr_prods_fk");

            entity.HasOne(d => d.IdProdazhaNavigation).WithMany(p => p.ProdazhaPrs)
                .HasForeignKey(d => d.IdProdazha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prodazha_pr_prodazha_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pk");

            entity.ToTable("role");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Table1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("table1");

            entity.Property(e => e.Column)
                .HasColumnType("character varying")
                .HasColumnName("column");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Passwd)
                .HasColumnType("character varying")
                .HasColumnName("passwd");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("users_role_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
