using Entities;

namespace ODataApp;

public interface IUserRepository
{
    IQueryable<User> GetAll();
    IQueryable<User> GetById(int id);
    Task Create(User user);
    Task BulkCreate(List<User> escos);
}
