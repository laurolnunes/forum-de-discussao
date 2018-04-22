using System.IO;
using Chatter.Domain.Categories;
using Chatter.Domain.Topics;
using Chatter.Domain.Users;
using Chatter.Infra.Data.Extensions;
using Chatter.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chatter.Infra.Data.Context
{
    public class ChatterContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserMapping());
            modelBuilder.AddConfiguration(new CategoryMapping());
            modelBuilder.AddConfiguration(new TopicMapping());
            modelBuilder.AddConfiguration(new PostMapping());
            modelBuilder.AddConfiguration(new LogMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}