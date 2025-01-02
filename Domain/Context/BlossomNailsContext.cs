using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public partial class BlossomNailsContext : DbContext
{
    public BlossomNailsContext(DbContextOptions<BlossomNailsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<ResetPassword> ResetPasswords { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceBooking> ServiceBookings { get; set; }

    public virtual DbSet<ServiceImage> ServiceImages { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingID).HasName("PK__Booking__73951ACDD3E12148");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingDate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasMaxLength(8);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.StartTime).HasMaxLength(8);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalCost).HasColumnType("money");
            entity.Property(e => e.UpdatedAT).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingCustomers)
                .HasForeignKey(d => d.CustomerID)
                .HasConstraintName("FK_Booking_Users");

            entity.HasOne(d => d.NailTechnician).WithMany(p => p.BookingNailTechnicians)
                .HasForeignKey(d => d.NailTechnicianID)
                .HasConstraintName("FK_Booking_Users1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryID).HasName("PK__Category__19093A2B105E0B6E");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactID).HasName("PK__Contact__5C6625BBE1CBC61D");

            entity.ToTable("Contact");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationID).HasName("PK__Notifica__20CF2E328DBB69F3");

            entity.ToTable("Notification");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("ntext");
            entity.Property(e => e.ReadAt).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Receiver)
                .HasConstraintName("FK_Notification_Users");
        });

        modelBuilder.Entity<ResetPassword>(entity =>
        {
            entity.HasKey(e => e.ResetPasswordID).HasName("PK__ResetPas__805BA242AD40866C");

            entity.ToTable("ResetPassword");

            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.ExpMinutes).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleID).HasName("PK__Role__8AFACE3A77CF3EA9");

            entity.ToTable("Role");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceID).HasName("PK__Service__C51BB0EA4F7A3AA0");

            entity.ToTable("Service");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RepresentativeImage).HasColumnType("ntext");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.WorkingTime).HasMaxLength(8);

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryID)
                .HasConstraintName("FK_Service_Category");
        });

        modelBuilder.Entity<ServiceBooking>(entity =>
        {
            entity.HasKey(e => e.ServiceBookingID).HasName("PK__ServiceB__78294814AD772F1D");

            entity.ToTable("ServiceBooking");

            entity.Property(e => e.ServiceCost).HasColumnType("money");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.BookingID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceBooking_Booking");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceBookings)
                .HasForeignKey(d => d.ServiceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceBooking_Service");
        });

        modelBuilder.Entity<ServiceImage>(entity =>
        {
            entity.HasKey(e => e.ImageID).HasName("PK__ServiceI__7516F4EC05DF6925");

            entity.ToTable("ServiceImage");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.ImageURL).HasColumnType("text");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceImages)
                .HasForeignKey(d => d.ServiceID)
                .HasConstraintName("FK_ServiceImage_Service");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenID).HasName("PK__Token__658FEE8A4047F95F");

            entity.ToTable("Token");

            entity.Property(e => e.RevokeAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_Token_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCACEC8A02E8");

            entity.Property(e => e.UserID).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Password).HasColumnType("text");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleID).HasName("PK__UserRole__3D978A55E9F699BD");

            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleID)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
