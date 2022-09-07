using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace test25_08.Models;

public class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
    
    public string? FullName { get; set; }

    public string? PassportNumber { get; set; }

    public DateTime BorNDate { get; set; }

    public bool IsAuthenticated { get; set; }
}