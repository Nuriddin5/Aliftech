using test25_08.Models;

namespace test25_08.DTOs;

public class RechargeResponse
{
    public int NumberOfRecharges { get; set; }
    public List<Recharge>? Recharges { get; set; }
    public double Total { get; set; }
    
}