using Gameverse.Services;
using Gameverse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Gameverse.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        var user = _service.GetById(id);

        if (user is not null)
        {
            return user;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("check")]
    public ActionResult<User> GetUserExists(string email, string password)
    {
        var user = _service.GetUserExists(email, password);

        if (user is not null)
        {
            return user;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("roles")]
    public IEnumerable<Role> GetRoles()
    {
        var Roles = _service.GetRoles();

        return Roles;
    }

    [HttpPost]
    public IActionResult Create([FromBody] UserDto newUser)
    {
        var user = _service.Create(newUser);
        return CreatedAtAction(nameof(GetById), new { id = user!.Id }, user);
    }

    [HttpPut("{id}/setrole")]
    public IActionResult SetRole(int id, int roleId)
    {
        var userToUpdate = _service.GetById(id);

        if (userToUpdate is not null)
        {
            _service.SetRole(id, roleId);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _service.GetById(id);

        if (user is not null)
        {
            _service.DeleteById(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{id}/purchaseHistory")]
    public IEnumerable<ShoppingCart> GetPurchaseHistory(int id)
    {
        var shoppingCarts = _service.GetPurchaseHistory(id);
        return shoppingCarts;
    }

    [HttpGet("{id}/shoppingCart")]

    public ShoppingCart GetShoppingCartByUserId(int id)
    {
        var shoppingCart = _service.GetShoppingCartByUserId(id);
        return shoppingCart;
    }
}