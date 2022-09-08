using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test25_08.Models;

public class Wallet
{
   
    public int Id { get; set; }


    [JsonIgnore] public User? Owner { get; set; }

    public double Balance { get; set; }
    
    // public Wallet(int id, double balance)
    // {
    //     Id = id;
    //     Balance = balance;
    // }

    
}