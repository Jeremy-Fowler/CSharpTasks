using System.Data;

namespace CSharpTasks.Repositories
{
  public class DbContext
  {
    protected readonly IDbConnection _db;

    public DbContext(IDbConnection db)
    {
      _db = db;
    }
  }
}