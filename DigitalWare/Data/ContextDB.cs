using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ContextDB : DbContext, IContextDB
    {
        private string _connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DigitalWare;Data Source=DESKTOP-MR52PR7\SQLEXPRESS";

        public ContextDB(DbContextOptions options) : base(options) { }
        public ContextDB() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!String.IsNullOrEmpty(_connectionString))
                options.UseSqlServer(_connectionString);
            base.OnConfiguring(options);
        }

        public virtual DbSet<Roles> roles { get; set; }
    }
}
