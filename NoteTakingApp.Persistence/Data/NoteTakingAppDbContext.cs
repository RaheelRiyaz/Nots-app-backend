using Microsoft.EntityFrameworkCore;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Persistence.Data
{
    public class NoteTakingAppDbContext : DbContext
    {
        public NoteTakingAppDbContext(DbContextOptions options) : base(options)
        {
        }




        #region Tables
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        #endregion Tables
    }

}
