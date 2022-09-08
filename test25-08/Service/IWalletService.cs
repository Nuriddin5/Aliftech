using test25_08.Models;

namespace test25_08.Service;

public interface IWalletService
{
    WalletResponse ReplenishForAuthenticated(int userId,int walletId, double amount);
    WalletResponse ReplenishForNonAuthenticated(int userId,int walletId, double amount);
}