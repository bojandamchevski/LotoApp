using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LotoAppDbContext : DbContext
    {
        public LotoAppDbContext(DbContextOptions<LotoAppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WinningNumber> WinningNumbers { get; set; }
        public DbSet<LotoNumber> LotoNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Admin relations

            modelBuilder.Entity<Admin>()
                .HasMany(x => x.Users)
                .WithOne(x => x.Admin)
                .HasForeignKey(x => x.AdminId);

            modelBuilder.Entity<Admin>()
                .HasMany(x => x.Draws)
                .WithOne(x => x.Admin)
                .HasForeignKey(x => x.AdminId);

            //Draws relations

            modelBuilder.Entity<Draw>()
                .HasOne(x => x.Admin)
                .WithMany(x => x.Draws)
                .HasForeignKey(x => x.AdminId);

            modelBuilder.Entity<Draw>()
                .HasOne(x => x.Session)
                .WithOne(x => x.Draw)
                .HasForeignKey<Draw>(x => x.SessionId);

            modelBuilder.Entity<Draw>()
                .HasMany(x => x.WinningNumbers)
                .WithOne(x => x.Draw)
                .HasForeignKey(x => x.DrawId);

            //Prizes relations

            modelBuilder.Entity<Prize>();

            //Sessions relations

            modelBuilder.Entity<Session>()
                .HasOne(x => x.Draw)
                .WithOne(x => x.Session)
                .HasForeignKey<Session>(x => x.DrawId);

            //Users relations

            modelBuilder.Entity<User>()
                .HasOne(x => x.Admin)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AdminId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.LotoNumbers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            //WinningNumbers relations

            modelBuilder.Entity<WinningNumber>()
                .HasOne(x => x.Draw)
                .WithMany(x => x.WinningNumbers)
                .HasForeignKey(x => x.DrawId);

            //WinningNumbers relations

            modelBuilder.Entity<LotoNumber>()
                .HasOne(x => x.User)
                .WithMany(x => x.LotoNumbers)
                .HasForeignKey(x => x.UserId);

            //Admin Validations

            modelBuilder.Entity<Admin>()
                .Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Admin>()
                .Property(x => x.Password)
                .HasMaxLength(50)
                .IsRequired();

            //User Validations

            modelBuilder.Entity<User>()
               .Property(x => x.Username)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .HasMaxLength(50)
                .IsRequired();

            //Prize seeding

            modelBuilder.Entity<Prize>()
                .HasData(new Prize()
                {
                    Id = 7,
                    PrizeType = "Car"
                },
                new Prize()
                {
                    Id = 6,
                    PrizeType = "Vacation"
                },
                new Prize()
                {
                    Id = 5,
                    PrizeType = "TV"
                },
                new Prize()
                {
                    Id = 4,
                    PrizeType = "100$ Gift Card"
                },
                new Prize()
                {
                    Id = 3,
                    PrizeType = "50$ GiftCard"
                });
        }
    }
}
