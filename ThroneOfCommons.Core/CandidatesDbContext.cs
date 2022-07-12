using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ThroneOfCommons.Core
{
   
    public class CandidatesDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Candidate> Candidates { get; set; }
        private PasswordHasher<ApplicationUser> _passwordHasher { get; set; }
        private readonly CandidatesDbContext _candidatesDbContext;

        public CandidatesDbContext(
            DbContextOptions<CandidatesDbContext> options) : base(options)
        {
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 1,
                Name = "Dishy",
                DateOfBirth= new DateTime(1980, 05, 12),
                LatestPortfolio="Finance",
                PartyType = 1,
                BiddedOn = DateTime.Now.Subtract(TimeSpan.FromDays(1))
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 2,
                Name = "Lizzie",
                DateOfBirth = new DateTime(1975, 07, 26),
                LatestPortfolio = "Foreign",
                PartyType=1,
                BiddedOn = DateTime.Now.Subtract(TimeSpan.FromDays(2))
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = 3,
                Name = "Benny",
                DateOfBirth = new DateTime(1970, 05, 15),
                LatestPortfolio = "Defence",
                PartyType = 1,
                BiddedOn = DateTime.Now.Subtract(TimeSpan.FromDays(3))
            });

            SeedApplicationUsers(modelBuilder);
        }

        private void SeedApplicationUsers(ModelBuilder modelBuilder)
        {
            var username = "Piers";
            var email = "admin@channel.com";
            var password = "PiersMax!!";

            var user = new ApplicationUser
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }


       //

    }
}