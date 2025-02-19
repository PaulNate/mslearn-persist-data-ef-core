using System.ComponentModel.DataAnnotations;

namespace Gameverse.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; }
    public Role? Role { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<ShoppingCart> shoppingCart { get; set; }
}