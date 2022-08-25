namespace test25_08.Models;

public class Wallet
{
    public int Id { get; set; }
    
    public User? Owner { get; set; }

    public double Balance { get; set; }

}