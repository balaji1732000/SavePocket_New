using Expense_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Db
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //The override keyword is used to indicate that a method in a derived class is overriding a
        //method in the base class, allowing you to provide your own implementation for that method.
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Account>(entity =>
        //    {
        //        entity.ToTable("Account");
        //    });
        //}
        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Account> Account { get; set; }
    }
}
