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
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<SessionStadistic> SessionStadistics { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

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



            /******************************************/
                    /*SESSION STADISTICS ENTITY*/
            /******************************************/
            builder.Entity<SessionStadistic>().ToTable("SessionStadistics");
            builder.Entity<SessionStadistic>().HasKey(s => new { s.FunctionalityId, s.SessionId });
            builder.Entity<SessionStadistic>().Property(s => s.Count);
            builder.Entity<SessionStadistic>()
                .HasOne(s => s.Functionality)
                .WithMany(f => f.SessionStadistics)
                .HasForeignKey(s => s.FunctionalityId);
            //builder.Entity<SessionStadistic>()
            //    .HasOne(s => s.Session)
            //    .WithMany(s => s.SessionStadistics)
            //    .HasForeignKey(s => s.SessionId);         //Esperando a que Session sea implementado



            /******************************************/
                    /*FUNCTIONALITY ENTITY*/
            /******************************************/
            builder.Entity<Functionality>().ToTable("Functionalities");
            builder.Entity<Functionality>().HasKey(f => f.Id);
            builder.Entity<Functionality>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Functionality>().Property(f => f.Name).IsRequired().HasMaxLength(20);
            builder.Entity<Functionality>().HasData
                (
                    new Functionality { Id = 1, Name = "Stream" },
                    new Functionality { Id = 2, Name = "Laser Pointer" },
                    new Functionality { Id = 3, Name = "Boards" },
                    new Functionality { Id = 4, Name = "Notes" },
                    new Functionality { Id = 5, Name = "Calendar" },
                    new Functionality { Id = 6, Name = "Alarm" },
                    new Functionality { Id = 7, Name = "ToDo List" }        
                );

            /******************************************/
                    /*NOTIFICATION ENTITY*/
            /******************************************/
            builder.Entity<Notification>().ToTable("Notifications");
            builder.Entity<Notification>().HasKey(n => n.Id);
            builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Notification>().Property(n => n.Date).IsRequired();
            builder.Entity<Notification>().Property(n => n.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Notification>().HasOne(n => n.Sender).WithMany(u => u.NotificationsSent).HasForeignKey(n => n.SenderId);
            builder.Entity<Notification>().HasOne(n => n.Remitend).WithMany(u => u.NotificationsReceived).HasForeignKey(n => n.RemitendId);

            /******************************************/
                    /*FRIENDSHIP ENTITY*/
            /******************************************/
            builder.Entity<Friendship>().ToTable("Friendships");
            builder.Entity<Friendship>().HasKey(f => new { f.user1Id, f.user2Id});
            builder.Entity<Friendship>().Property(f => f.user1Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Friendship>().Property(f => f.user2Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<Friendship>().HasOne(f => f.).WithMany(f => f.);



            /******************************************/
                    /*GROUP ENTITY*/
            /******************************************/
            builder.Entity<Group>().ToTable("Groups");
            builder.Entity<Group>().HasKey(p => p.Id);
            builder.Entity<Group>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Group>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            /******************************************/
                    /*GROUPMEMBER ENTITY*/
            /******************************************/
            builder.Entity<GroupMember>().ToTable("GroupMembers");
            builder.Entity<GroupMember>().HasKey(p => new { p.UserId, p.GroupId });
            builder.Entity<GroupMember>().Property(p => p.UserCreator).IsRequired();

            builder.Entity<GroupMember>()
                .HasOne(pt => pt.Group)
                .WithMany(p => p.GroupMembers)
                .HasForeignKey(pt => pt.GroupId);

            builder.Entity<GroupMember>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.GroupMembers)
                .HasForeignKey(pt => pt.UserId);

            //Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
