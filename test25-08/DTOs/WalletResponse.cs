namespace test25_08.Models;

public class WalletResponse
{
    public WalletResponse(object property, string? message)
    {
        Message = message;
        Property = property;
    } 
    public WalletResponse(object property)
    {
        Property = property;
    }
    public WalletResponse(string? message)
    {
        Message = message;
    }

    public Object? Property { get; set; }

    public string? Message { get; set; }
}