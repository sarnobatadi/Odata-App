using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ODataApp;

public class UsersController(IUserRepository userRepository) : ODataController
{
    [EnableQuery]
    public IQueryable<User> Get()
    {
        return userRepository.GetAll();
    }

    [EnableQuery]
    public IQueryable<User> Get(int key)
    {
        return userRepository.GetById(key);
    }

    public async Task<IActionResult> Post([FromBody] User User)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await userRepository.Create(User);
        return Created(User);
    }


    [HttpPost("odata/Users/bulk-create")]
    public async Task<IActionResult> BulkCreate([FromBody] List<User> Users)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await userRepository.BulkCreate(Users);
        return Ok();
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await userRepository.Delete(key);

        return Ok();
    }

    public async Task<IActionResult> Put([FromODataUri] int key, User update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await userRepository.Update(key, update);
        return Ok();

    }

}