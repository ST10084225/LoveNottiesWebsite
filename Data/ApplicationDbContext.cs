using NottiesRebuiltV3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NottiesRebuiltV3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<BlogItem> BlogItems { get; set; }
        public DbSet<OurPeople> OurPeoples { get; set; }
        public DbSet<SuccessStory> successStories { get; set; }
        public DbSet<WhatsOnModel> WhatsOn { get; set; }
        public DbSet<NewsLetterModel> NewsLetterEmails { get; set; }


        //Seeding --> 2 tables --categories --> onModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogItem>().HasKey(blogItems => new { blogItems.BlogID });
            modelBuilder.Entity<OurPeople>().HasKey(ourPeople => new { ourPeople.OurPersonID });
            modelBuilder.Entity<SuccessStory>().HasKey(successStories => new { successStories.SuccessStoryID });
            modelBuilder.Entity<WhatsOnModel>().HasKey(WhatsOn => new { WhatsOn.PostID });
            modelBuilder.Entity<NewsLetterModel>().HasKey(NewsLetterEmails => new { NewsLetterEmails.EmailID });


            //Seeding roles to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Manager", NormalizedName = "Manager".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            //Seed a Manager Account
            //Seeding the Manager User to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "1", // primary key
                    Email = "lovenotties@gmail.com",
                    NormalizedEmail = "LOVENOTTIES@gmail.com".ToUpper(),
                    UserName = "NottiesAdmin",
                    NormalizedUserName = "NOTTIESADMIN",
                    PasswordHash = hasher.HashPassword(null, "MissySue01#")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "1"
                }
            );

        }

    }
}
