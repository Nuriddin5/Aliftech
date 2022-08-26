using System.ComponentModel.DataAnnotations;

namespace test25_08.Models;

public class UserCredentials
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
}