using System.ComponentModel.DataAnnotations;

namespace TableStore.Models;
public class User
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
