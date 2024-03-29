﻿using EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.username).IsUnique();

        modelBuilder.Entity<Hobby>().HasIndex(h => h.hobbyName).IsUnique();

        modelBuilder.Entity<Event>().HasIndex(e => e.eventName).IsUnique();
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Event> Events  { get; set; }
    public DbSet<EventAttendee> EventAttendees { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<Followers> Followers { get; set; }
    public DbSet<HobbiesOfUsers> HobbiesOfUsers { get; set; }
}