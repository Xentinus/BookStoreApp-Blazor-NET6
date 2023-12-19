using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreDBContext : IdentityDbContext<ApiUser> // Use ApiUser not the regular Identity user (IdentityUser)
    {
        public BookStoreDBContext()
        {
        }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA24C652B6")
                    .IsUnique();

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
            });

            // Create roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "2380b3f3-5150-4bfc-8a15-1bce86ee6781"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "62bf172a-22c1-4068-ba80-e0346921565d"
                });

            // Hasher to hash password
            var hasher = new PasswordHasher<ApiUser>();

            // Create default entities
            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "d145be23-6977-4b7d-b1bf-4395ec8a3c13",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                },
                new ApiUser
                {
                    Id = "2aab5595-65a6-470a-be6f-10779f5f9b7b",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                });

            // Assign user to role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2380b3f3-5150-4bfc-8a15-1bce86ee6781",
                    UserId = "2aab5595-65a6-470a-be6f-10779f5f9b7b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "62bf172a-22c1-4068-ba80-e0346921565d",
                    UserId = "d145be23-6977-4b7d-b1bf-4395ec8a3c13"
                });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
