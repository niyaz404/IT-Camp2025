using System.Data;
using System.Data.Common;

namespace AuthService.DAL.Repositories.Abstract;

public interface IRepository
{
    IDbConnection CreateConnection();
}