using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Reimbursement> Reimbursements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<ReimbursementHistory> ReimbursementHistories { get; set; }

        //define relations here using modelbuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //employee account
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(a => a.NIK);

            //account accountrole
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(b => b.Account);

            //accountrole role
            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Role)
                .WithMany(b => b.AccountRoles);

            //reimbursement employee
            modelBuilder.Entity<Reimbursement>()
                .HasOne(a => a.Employee)
                .WithMany(b => b.Reimbursements);

            //reimbursement category
            modelBuilder.Entity<Reimbursement>()
                .HasOne(a => a.Category)
                .WithMany(b => b.Reimbursements);

            //reimbursement status
            modelBuilder.Entity<Reimbursement>()
                .HasOne(a => a.Status)
                .WithMany(b => b.Reimbursements);

            //reimbursement statushistory
            modelBuilder.Entity<Reimbursement>()
                .HasMany(b => b.StatusHistories)
                .WithOne(a => a.Reimbursement);

            //reimbursement reimbursementhistory
            modelBuilder.Entity<Reimbursement>()
                .HasMany(b => b.ReimbursementHistories)
                .WithOne(a => a.Reimbursement);

            //status statushistory
            modelBuilder.Entity<Status>()
                .HasMany(b => b.StatusHistories)
                .WithOne(a => a.Status);

            //status reimbursementhistory
            modelBuilder.Entity<Status>()
                .HasMany(b => b.ReimbursementHistories)
                .WithOne(a => a.Status);
        }
    }
}
