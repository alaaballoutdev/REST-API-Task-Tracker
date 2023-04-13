using Microsoft.EntityFrameworkCore;

namespace TaskTrackerApp.Model
{
    public class TaskDb : DbContext
    {

        protected readonly IConfiguration Configuration;

        public TaskDb(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseMySQL(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<TaskModel> Tasks { get; set; }



    }
}
