using System;
using FSHomeWork.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSHomeWork.DAL.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Email = "first@gmail.com",
                    Password = "password",
                    UserId = 1
                },
                new User()
                {
                    Email = "second@gmail.com",
                    Password = "password",
                    UserId = 2
                },
                new User()
                {
                    Email = "third@gmail.com",
                    Password = "password",
                    UserId = 3
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    MessageId = 1,
                    MessageText = "from 1 to 2",
                    SumOfBothId = 3,
                    UserId = 1,
                    Time = DateTime.Now
                },
                new Message
                {
                    MessageId = 2,
                    MessageText = "from 1 to 3",
                    SumOfBothId = 4,
                    UserId = 1,
                    Time = DateTime.Now
                },
                new Message
                {
                    MessageId = 3,
                    MessageText = "from 2 to 1",
                    SumOfBothId = 3,
                    UserId = 2,
                    Time = DateTime.Now
                },
                new Message
                {
                    MessageId = 4,
                    MessageText = "from 2 to 3",
                    SumOfBothId = 5,
                    UserId = 2,
                    Time = DateTime.Now
                },
                new Message
                {
                    MessageId = 5,
                    MessageText = "from 3 to 1",
                    SumOfBothId = 4,
                    UserId = 3,
                    Time = DateTime.Now
                },
                new Message
                {
                    MessageId = 6,
                    MessageText = "from 3 to 2",
                    SumOfBothId = 5,
                    UserId = 3,
                    Time = DateTime.Now
                }
                
            );



        }
    }
}