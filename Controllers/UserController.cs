using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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

    [HttpGet("thus")]
    public string getTest()
    {
        return "SEND";
    }

    // [HttpPost("odata/Users/bulk-create")]
    // public async Task<IActionResult> BulkCreate([FromBody] List<User> Users)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     await userRepository.BulkCreate(Users);
    //     return Ok();
    // }

}