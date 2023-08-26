using Microsoft.EntityFrameworkCore;
using WebAppExpenses.Models.Domain;

namespace WebAppExpenses.Data
{
    public class AppDemoDbContext : DbContext
    {
        public AppDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        internal Task GetExpensesBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
