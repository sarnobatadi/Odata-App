using Entities;
using Microsoft.EntityFrameworkCore;

namespace ODataApp;

public class UserRepository : IUserRepository
{
    DataContextEF context;

    public UserRepository(IConfiguration config)
    {
        context = new DataContextEF(config);
    }
    public IQueryable<User> GetAll()
    {
        Console.WriteLine("ALL");
        Console.WriteLine(context.Users.AsQueryable());
        return context.Users.AsQueryable();
    }

    public IQueryable<User> GetById(int id)
    {
        return context.Users.Where(x => x.UserId == id);
    }

    public async Task Create(User User)
    {
        context.Users.Add(User);
        await context.SaveChangesAsync();
    }

    public async Task BulkCreate(List<User> Users)
    {
        await context.Users.AddRangeAsync(Users);
        await context.SaveChangesAsync();
    }
}
