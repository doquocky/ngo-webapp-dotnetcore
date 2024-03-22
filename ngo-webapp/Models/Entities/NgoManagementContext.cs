﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ngo_webapp.Models;

namespace ngo_webapp.Models.Entities;

public partial class NgoManagementContext : DbContext
{
    public NgoManagementContext()
    {
    }

    public NgoManagementContext(DbContextOptions<NgoManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appeal> Appeals { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

		=> optionsBuilder.UseSqlServer("Data Source=DEVBLOCK; Initial Catalog=Ngo-Management;Persist Security Info=True;User ID=sa;Password=05012004;Encrypt=True;Trust Server Certificate=True");


	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appeal>(entity =>
        {
            entity.HasKey(e => e.AppealsId).HasName("PK__Appeals__5E813B3049020FB9");

            entity.HasIndex(e => e.AppealsName, "UC_Appeals_AppealsName").IsUnique();

            entity.Property(e => e.AppealsId).HasColumnName("AppealsID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AppealsImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Appeals_Image");
            entity.Property(e => e.AppealsName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Organization)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Blog>(entity =>
        {

            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E508A296491");


            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.AppealId).HasColumnName("AppealID");
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Appeal).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AppealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blogs_Appeals");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Blogs_Users");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAAA09D8422");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ParentCommentId).HasColumnName("ParentCommentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Blogs");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comments_ParentComment");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Donation>(entity =>
        {

            entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EDB43EA259E");


            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AppealsId).HasColumnName("AppealsID");
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Appeals).WithMany(p => p.Donations)
                .HasForeignKey(d => d.AppealsId)
                .HasConstraintName("FK_Donations_Appeals");

            entity.HasOne(d => d.User).WithMany(p => p.Donations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Donations_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC7F82A773");


            entity.HasIndex(e => e.Email, "UC_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UC_Users_Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GoogleHash)
                .HasMaxLength(255)
                .HasColumnName("Google_Hash");
            entity.Property(e => e.IsAdmin).HasColumnName("Is_Admin");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("User_Image");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<ngo_webapp.Models.BlogViewModelAD> BlogViewModelAD { get; set; } = default!;
}
