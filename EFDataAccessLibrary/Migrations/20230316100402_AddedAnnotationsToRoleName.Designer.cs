﻿// <auto-generated />
using System;
using EFDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFDataAccessLibrary.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230316100402_AddedAnnotationsToRoleName")]
    partial class AddedAnnotationsToRoleName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFDataAccessLibrary.Models.Hobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("hobbyName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Hobbies");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("passwordHash")
                        .HasMaxLength(500)
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Hobby", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.User", null)
                        .WithMany("Hobbies")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EFDataAccessLibrary.Models.Role", b =>
                {
                    b.HasOne("EFDataAccessLibrary.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
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
