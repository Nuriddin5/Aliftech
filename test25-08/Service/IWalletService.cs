using test25_08.Models;

namespace test25_08.Service;

public interface IWalletService
{
    WalletResponse replenishForAuthenticated(int walletId, double amount);
    WalletResponse replenishForNonAuthenticated(int walletId, double amount);
}