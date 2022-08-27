using test25_08.Models;

namespace test25_08.Service;

public class WalletService : IWalletService
{
    public WalletResponse replenishForAuthentificated(int walletId, double amount)
    {
        throw new NotImplementedException();
    }

    public WalletResponse replenishForNonAuthentificated(int walletId, double amount)
    {
        throw new NotImplementedException();
    }
}