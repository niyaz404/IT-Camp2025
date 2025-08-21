using System.Data;

namespace DataManagerService.DAL.Repositories.Abstract;

public interface IRepository
{
    IDbConnection CreateConnection();
}