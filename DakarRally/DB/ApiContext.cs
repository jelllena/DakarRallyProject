using Microsoft.EntityFrameworkCore;

namespace DakarRally
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
        public DbSet<DakarRally.DbModel.Vehicle> Vehicle { get; set; }

        public DbSet<DakarRally.DbModel.Race> Race { get; set; }

        public DbSet<DakarRally.DbModel.RaceVehicleStatistic> RaceVehicleStatistic { get; set; }

    }
}
