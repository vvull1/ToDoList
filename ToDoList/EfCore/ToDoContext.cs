using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.EfCore
{
    public class ToDoContext :DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<Messaging> Messagings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoggerTable> Loggers { get; set; }
        public DbSet<ActivityLogger> ActivityLoggers { get; set; }
        public DbSet<TaskTable> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Helper.ConnectionString);
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Messaging>(x =>
        //    {
        //        x.HasOne(x => x.Sender).WithMany(m => m.SenderMsg).OnDelete(DeleteBehavior.Restrict);
        //        x.HasOne(x => x.Receiver).WithMany(m => m.ReceiverMsg).OnDelete(DeleteBehavior.Restrict);
        //    });
        //}
    }
}
