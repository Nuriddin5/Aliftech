using Microsoft.EntityFrameworkCore;
using test25_08.Models;

namespace test25_08.Service;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _context;

    public WalletService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public WalletResponse ReplenishForAuthenticated(int walletId, double amount)
    {
        var wallet = _context.Wallet?.Find(walletId);
        if (wallet == null )
        {
            return new WalletResponse(false, "Wrong with server,try again later");
        }
        if (wallet.Balance + amount <= 100000 )
        {
            wallet.Balance += amount;
            // var recharge = new Recharge(amount,DateTime.Now, true);
            
            
            // _context.Entry(recharge).State = EntityState.Modified;
            _context.Entry(wallet).State = EntityState.Modified;
            
            _context.SaveChanges();
            
            return new WalletResponse(true, $"Wallet successfully replenished to {amount}");
        }

        return new WalletResponse(false, "You cant have balance over 100000");
        
    }

    public  WalletResponse ReplenishForNonAuthenticated(int walletId, double amount)
    {
        var wallet = _context.Wallet?.Find(walletId);
        if (wallet == null )
        {
            return new WalletResponse(false, "Wrong with server,try again later");
        }
        if (wallet.Balance + amount <= 10000)
        {
            wallet.Balance += amount;
            // var recharge = new Recharge(amount,DateTime.Now, true);
            
            
            // _context.Entry(recharge).State = EntityState.Modified;
            _context.Entry(wallet).State = EntityState.Modified;
            
            _context.SaveChanges();
            
            return new WalletResponse(true, $"Wallet successfully replenished to {amount}");
        }

        return new WalletResponse(false, "You cant have balance over 10000");
    }
}