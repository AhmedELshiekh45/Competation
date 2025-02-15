using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Base;
using DataAccess_Layer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer.Context
{
    public class MyContext : IdentityDbContext<User>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Exam>()
               .HasOne(e => e.Entry)
               .WithMany()
               .HasForeignKey(e => e.EntryId)
               .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Exam>()
            .HasOne(e => e.Teacher)
            .WithMany(t => t.ContestantsExams)
            .HasForeignKey(e => e.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);
            // Set foreign key to null
            //builder.Entity<Exam>()
            // .HasOne(e => e.Teacher)
            // .WithMany()
            // .HasForeignKey(e => e.TeacherId)
            // .OnDelete(DeleteBehavior.SetNull); // Set the foreign key to null instead of deleting

            base.OnModelCreating(builder);
        }
        public DbSet<Contestant> Contestants { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<PartRegistration> PartRegistrations { get; set; }


        public override int SaveChanges()
        {
            HandleTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseClass>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Id = Guid.NewGuid().ToString();
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }
        }



    }

}

