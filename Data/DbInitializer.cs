using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDBContext _dbContext;

        public DbInitializer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
