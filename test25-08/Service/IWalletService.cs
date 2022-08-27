using test25_08.Models;

namespace test25_08.Service;

public interface IWalletService
{
    WalletResponse replenishForAuthentificated(int walletId, double amount);
    WalletResponse replenishForNonAuthentificated(int walletId, double amount);
}