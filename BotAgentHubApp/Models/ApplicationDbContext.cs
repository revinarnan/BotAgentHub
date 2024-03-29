﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BotAgentHubApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ChatbotConfiguration> ChatbotConfigurations { get; set; }
        public DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<ChatHistory> ChatHistories { get; set; }
        public DbSet<ChatBotEmailQuestion> ChatBotEmailQuestions { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow.AddHours(7).ToString("MM/dd/yyyy hh:mm:ss tt"); // current datetime WIB

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                    ((BaseEntity)entity.Entity).Date = DateTime.UtcNow.AddHours(7).ToString("MM/dd/yyyy");
                    ((BaseEntity)entity.Entity).Time = DateTime.UtcNow.AddHours(7).ToString("hh:mm:ss tt");
                }

                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}