using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Converters;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Extensions;

namespace UltimateTeamApi.Domain.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /******************************************/
                        /*USER ENTITY*/
            /******************************************/
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(u => u.UserName).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(60);
            builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(15);
            builder.Entity<User>().Property(u => u.Birthdate).IsRequired();
            builder.Entity<User>().Property(u => u.LastConnection).IsRequired();
            builder.Entity<User>().Property(u => u.ProfilePicture).HasConversion(
                picture => BitmapConverter.BitmapToByteArray(picture),          //Save as Byte Array
                byteArr => BitmapConverter.ByteArrayToBitmap(byteArr));         //Get as Bitmap
            //builder.Entity<User>()
            //    .HasOne(u => u.Administrator)
            //    .WithMany(a => a.Users)
            //    .HasForeignKey(u => u.AdministratorId);       //Esperando a que Administrator sea implementado



            /******************************************/
                        /*ADMINISTRATOR ENTITY*/
            /******************************************/
            builder.Entity<Administrator>().ToTable("Administrators");
            builder.Entity<Administrator>().HasKey(a => a.Id);
            builder.Entity<Administrator>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            //Agregar cosas




            //Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
