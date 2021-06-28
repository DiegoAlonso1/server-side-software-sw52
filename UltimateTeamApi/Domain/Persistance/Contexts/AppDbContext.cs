using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Converters;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Extensions;

namespace UltimateTeamApi.Domain.Persistance.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<SessionStadistic> SessionStadistics { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionParticipant> SessionParticipants { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<SubscriptionBill> SubscriptionBills { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            /******************************************/
                            /*PERSON ENTITY*/
            /******************************************/
            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Person>().Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(20);
            builder.Entity<Person>().Property(p => p.UserName).IsRequired().HasMaxLength(20);
            builder.Entity<Person>().Property(p => p.Email).IsRequired().HasMaxLength(60);
            builder.Entity<Person>().Property(p => p.Password).IsRequired().HasMaxLength(15);
            builder.Entity<Person>().Property(p => p.Birthdate).IsRequired();
            builder.Entity<Person>().Property(p => p.LastConnection).IsRequired();
            builder.Entity<Person>().Property(p => p.ProfilePicture).HasConversion(
                picture => BitmapConverter.BitmapToByteArray(picture),          //Save as Byte Array
                byteArr => BitmapConverter.ByteArrayToBitmap(byteArr));         //Get as Bitmap
            builder.Entity<Person>()
            .HasOne(p => p.Administrator)
            .WithMany(a => a.Persons)
            .HasForeignKey(p => p.AdministratorId);



            /******************************************/
                        /*ADMINISTRATOR ENTITY*/
            /******************************************/
            builder.Entity<Administrator>().ToTable("Administrators");
            builder.Entity<Administrator>().HasKey(a => a.Id);
            builder.Entity<Administrator>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Administrator>().Property(a => a.Name).IsRequired();
            builder.Entity<Administrator>().Property(a => a.Password).IsRequired();
            builder.Entity<Administrator>().Property(a => a.Area).IsRequired();



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
            builder.Entity<SessionStadistic>()
                .HasOne(s => s.Session)
                .WithMany(s => s.SessionStadistics)
                .HasForeignKey(s => s.SessionId);



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
            builder.Entity<Notification>().HasOne(n => n.Sender).WithMany(p => p.NotificationsSent).HasForeignKey(n => n.SenderId);
            builder.Entity<Notification>().HasOne(n => n.Remitend).WithMany(p => p.NotificationsReceived).HasForeignKey(n => n.RemitendId);



            /******************************************/
                     /*FRIENDSHIP ENTITY*/
            /******************************************/
            builder.Entity<Friendship>().ToTable("Friendships");
            builder.Entity<Friendship>().HasKey(f => new { f.PrincipalId, f.FriendId});
            builder.Entity<Friendship>()
                .HasOne(f => f.Principal)
                .WithMany(p => p.FriendShipsAsPrincipal)
                .HasForeignKey(f => f.PrincipalId);
            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(p => p.FriendShipsAsFriend)
                .HasForeignKey(f => f.FriendId);



            /******************************************/
                    /*GROUP ENTITY*/
            /******************************************/
            builder.Entity<Group>().ToTable("Groups");
            builder.Entity<Group>().HasKey(g => g.Id);
            builder.Entity<Group>().Property(g => g.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Group>().Property(g => g.Name).IsRequired().HasMaxLength(30);



            /******************************************/
                    /*GROUPMEMBER ENTITY*/
            /******************************************/
            builder.Entity<GroupMember>().ToTable("GroupMembers");
            builder.Entity<GroupMember>().HasKey(gm => new { gm.PersonId, gm.GroupId });
            builder.Entity<GroupMember>().Property(gm => gm.PersonCreator).IsRequired();
            builder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.GroupMembers)
                .HasForeignKey(gm => gm.GroupId);
            builder.Entity<GroupMember>()
                .HasOne(gm => gm.Person)
                .WithMany(p => p.GroupMembers)
                .HasForeignKey(gm => gm.PersonId);



            /******************************************/
                        /*SESSION ENTITY*/
            /******************************************/
            builder.Entity<Session>().ToTable("Sessions");
            builder.Entity<Session>().HasKey(s => s.Id);
            builder.Entity<Session>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Session>().Property(s => s.Name).IsRequired().HasMaxLength(20);
            builder.Entity<Session>()
                .HasOne(s => s.SessionType)
                .WithMany(st => st.Sessions)
                .HasForeignKey(s => s.SessionTypeId);



            /******************************************/
                    /*SESSIONPARTICIPANT ENTITY*/
            /******************************************/
            builder.Entity<SessionParticipant>().ToTable("SessionParticipants");
            builder.Entity<SessionParticipant>().HasKey(sp => new { sp.SessionId, sp.PersonId });
            builder.Entity<SessionParticipant>().Property(sp => sp.Creator).IsRequired();
            builder.Entity<SessionParticipant>()
                .HasOne(sp => sp.Session)
                .WithMany(s => s.SessionParticipants)
                .HasForeignKey(sp => sp.SessionId);
            builder.Entity<SessionParticipant>()
                .HasOne(sp => sp.Person)
                .WithMany(p => p.SessionsParticipated)
                .HasForeignKey(sp => sp.PersonId);



            /******************************************/
                    /*SESSIONTYPE ENTITY*/
            /******************************************/
            builder.Entity<SessionType>().ToTable("SessionTypes");
            builder.Entity<SessionType>().HasKey(st => st.Id);
            builder.Entity<SessionType>().Property(st => st.Id).IsRequired();
            builder.Entity<SessionType>().Property(st => st.Type).IsRequired().HasMaxLength(15);
            builder.Entity<SessionType>().HasData
                (
                    new SessionType { Id = 1, Type = "Collaborative" },
                    new SessionType { Id = 2, Type = "Individual" }
                );



            /******************************************/
                        /*SUBSCRIPTION TYPE*/
            /******************************************/
            builder.Entity<SubscriptionType>().ToTable("SubscriptionTypes");
            builder.Entity<SubscriptionType>().HasKey(s => s.Id);
            builder.Entity<SubscriptionType>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubscriptionType>().Property(s => s.Type).IsRequired();
            builder.Entity<SubscriptionType>().Property(s => s.Amount).IsRequired();
            builder.Entity<SubscriptionType>()
                .HasMany(st => st.SubscriptionBills)
                .WithOne(sb => sb.SubscriptionType)
                .HasForeignKey(sb => sb.SubscriptionTypeId);



            /******************************************/
                        /*SUBSCRIPTION BILL*/
            /******************************************/
            builder.Entity<SubscriptionBill>().ToTable("SubscriptionBills");
            builder.Entity<SubscriptionBill>().HasKey(s => s.Id);
            builder.Entity<SubscriptionBill>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubscriptionBill>().Property(s => s.License).IsRequired();
            builder.Entity<SubscriptionBill>().Property(s => s.ActiveLicense).IsRequired();
            builder.Entity<SubscriptionBill>().Property(s => s.DeadlineLicense);
            builder.Entity<SubscriptionBill>().Property(s => s.PeriodMonths).IsRequired();
            builder.Entity<SubscriptionBill>().Property(s => s.Paid).IsRequired();
            builder.Entity<SubscriptionBill>().Property(s => s.PaymentMethod).IsRequired();


            //Apply Naming Convention
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
