using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataLayer.DBContext
{
    public partial class db_aad141_swd392Context : DbContext
    {
        public db_aad141_swd392Context()
        {
        }

        public db_aad141_swd392Context(DbContextOptions<db_aad141_swd392Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Entities.Club> Clubs { get; set; }
        public virtual DbSet<Entities.Inventory> Inventories { get; set; }
        public virtual DbSet<Entities.Notification> Notifications { get; set; }
        public virtual DbSet<Entities.Order> Orders { get; set; }
        public virtual DbSet<Entities.OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Entities.Payment> Payments { get; set; }
        public virtual DbSet<Entities.Player> Players { get; set; }
        public virtual DbSet<Entities.Session> Sessions { get; set; }
        public virtual DbSet<Entities.Shirt> Shirts { get; set; }
        public virtual DbSet<Entities.TypeShirt> TypeShirts { get; set; }
        public virtual DbSet<Entities.User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        string GetConnectionString()
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return builder["ConnectionStrings:hosting"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Club>(entity =>
            {
                entity.ToTable("Club");

                entity.Property(e => e.ClubLogo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EstablishedYear).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StadiumName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Entities.Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ShirtId).HasColumnName("Shirt_Id");

                entity.HasOne(d => d.Shirt)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ShirtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Shirt__34C8D9D1");
            });

            modelBuilder.Entity<Entities.Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Notificat__User___37A5467C");
            });

            modelBuilder.Entity<Entities.Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__User_Id__3A81B327");
            });

            modelBuilder.Entity<Entities.OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.ShirtId).HasColumnName("Shirt_Id");

                entity.Property(e => e.StatusRating).HasColumnName("Status_Rating");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__4222D4EF");

                entity.HasOne(d => d.Shirt)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ShirtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Shirt__412EB0B6");
            });

            modelBuilder.Entity<Entities.Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__Order_I__3E52440B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__User_Id__3D5E1FD2");
            });

            modelBuilder.Entity<Entities.Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.ClubId).HasColumnName("Club_Id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK__Player__Club_Id__286302EC");
            });

            modelBuilder.Entity<Entities.Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.EndDdate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDdate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Entities.Shirt>(entity =>
            {
                entity.ToTable("Shirt");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PlayerId).HasColumnName("Player_Id");

                entity.Property(e => e.TypeShirtId).HasColumnName("TypeShirt_Id");

                entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Shirts)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shirt__Player_Id__30F848ED");

                entity.HasOne(d => d.TypeShirt)
                    .WithMany(p => p.Shirts)
                    .HasForeignKey(d => d.TypeShirtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shirt__TypeShirt__31EC6D26");
            });

            modelBuilder.Entity<Entities.TypeShirt>(entity =>
            {
                entity.ToTable("TypeShirt");

                entity.Property(e => e.ClubId).HasColumnName("Club_Id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SessionId).HasColumnName("Session_Id");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.TypeShirts)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TypeShirt__Club___2E1BDC42");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.TypeShirts)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TypeShirt__Sessi__2D27B809");
            });

            modelBuilder.Entity<Entities.User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address)
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50);

                entity.Property(e => e.ImgUrl)
                    .HasColumnName("ImgURL");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("date")
                    .HasColumnName("Modified_Date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.RatingCount).HasColumnName("Rating_Count");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.IsVerify)
                .HasColumnName("IsVerify")
                .IsRequired();
                entity.Property(e => e.IsDelete)
                .HasColumnName("IsDelete")
                .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
