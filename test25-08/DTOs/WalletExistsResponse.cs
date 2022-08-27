namespace test25_08.Models;

public class WalletExistsResponse
{
    public WalletExistsResponse(bool isExists, string? message)
    {
        IsExists = isExists;
        Message = message;
    }

    public bool IsExists { get; set; }

    public string? Message { get; set; }
}