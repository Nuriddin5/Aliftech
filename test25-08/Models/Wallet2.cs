using System.Text.Json.Serialization;

namespace test25_08.Models;

public class Wallet2
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    [JsonIgnore]
    public User? Owner { get; set; }

    public double Balance { get; set; }
}