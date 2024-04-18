using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    public async Task Delete(int id)
    {
        var user = await context.Users.FindAsync(id);
        context.Users.Remove(user);
        await context.SaveChangesAsync();

    }

    public async Task Update(int key, User update)
    {
        var user = context.Users.SingleOrDefault(d => d.UserId == key);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.FirstName = update.FirstName;
        user.LastName = update.LastName;
        user.Active = update.Active;
        user.Email = update.Email;
        user.Gender = update.Gender;
        await context.SaveChangesAsync();

    }

}
