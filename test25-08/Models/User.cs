namespace test25_08.Models;

public class User
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual DateTime? LastLoginTime { get; set; }

    public virtual DateTime? RegistrationDate { get; set; }

    public Wallet2 Wallet2 { get; set; }

    // public bool IsBlocked { get; set; }
}