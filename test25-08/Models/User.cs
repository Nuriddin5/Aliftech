using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace test25_08.Models;

public class User : IdentityUser
{
    public int NumberId { get; set; }

    public string? FullName { get; set;}

    public string? PassportNumber { get; set; }

    public DateTime BorNDate { get; set; }

    public bool IsAuthenticated { get; set; }
    
}