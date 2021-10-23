using System.Data;
using Bloggr.Models;
using Dapper;

namespace Bloggr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile Get(string accountId)
    {
      return _db.QueryFirstOrDefault<Profile>("SELECT * FROM accounts WHERE id = @accountId", new { accountId });
    }
  }
}