namespace test25_08.Models;

public class WalletResponse
{
    public WalletResponse(bool property, object? resultOrMessage)
    {
        ResultOrMessage = resultOrMessage;
        Success = property;
    } 
    public WalletResponse(bool property)
    {
        Success = property;
    }
    public WalletResponse(object? message)
    {
        ResultOrMessage = message;
    }

    public bool? Success { get; set; }

    public object? ResultOrMessage { get; set; }
}