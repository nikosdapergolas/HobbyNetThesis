﻿// <auto-generated />
using System;
using EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFDataAccessLibrary.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFDataAccessLibrary.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("lastMessageSenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("lastMessageSenderId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.ChatMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<int>("memberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("memberId");

                    b.ToTable("ChatMembers");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("userId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("hobbyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("eventName")
                        .IsUnique();

                    b.HasIndex("hobbyId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.EventAttendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("attendeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("attendeeId");

                    b.ToTable("EventAttendees");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.HobbiesOfUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HobbyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HobbiesOfUsers");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Hobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("hobbyName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("hobbyName")
                        .IsUnique();

                    b.ToTable("Hobbies");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("userId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("messageBody")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HobbyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("profileImage")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Chat", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.User", "lastMessageSender")
                        .WithMany()
                        .HasForeignKey("lastMessageSenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lastMessageSender");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.ChatMember", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Chat", null)
                        .WithMany("members")
                        .HasForeignKey("ChatId");

                    b.HasOne("EFDataAccessLibrary.Models.User", "member")
                        .WithMany()
                        .HasForeignKey("memberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("member");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Comment", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Post", null)
                        .WithMany("comments")
                        .HasForeignKey("PostId");

                    b.HasOne("EFDataAccessLibrary.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Event", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Hobby", "hobby")
                        .WithMany()
                        .HasForeignKey("hobbyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hobby");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.EventAttendee", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Event", null)
                        .WithMany("attendees")
                        .HasForeignKey("EventId");

                    b.HasOne("EFDataAccessLibrary.Models.User", "attendee")
                        .WithMany()
                        .HasForeignKey("attendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("attendee");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.HobbiesOfUsers", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.User", null)
                        .WithMany("Hobbies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Like", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Post", null)
                        .WithMany("postLikes")
                        .HasForeignKey("PostId");

                    b.HasOne("EFDataAccessLibrary.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Message", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.Chat", null)
                        .WithMany("chatMessages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Role", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Chat", b =>
                {
                    b.Navigation("chatMessages");

                    b.Navigation("members");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Event", b =>
                {
                    b.Navigation("attendees");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Post", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("postLikes");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.User", b =>
                {
                    b.Navigation("Hobbies");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
