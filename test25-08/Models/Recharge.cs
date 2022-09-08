using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace test25_08.Models;

public class Recharge
{
    [Key]
    public int Id { get; set; }
    
    [JsonIgnore] private User? Recharger { get; set; }
    
     public double Amount { get; set; }

    public DateTime DateTime { get; set; }
    
    public bool IsIncome { get; set; }
    
    // public Recharge(double amount, DateTime dateTime, bool isIncome)
    // {
    //     Amount = amount;
    //     DateTime = dateTime;
    //     IsIncome = isIncome;
    // }
}