using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test25_08.DTOs;
using test25_08.Models;

namespace test25_08.Service;

public class WalletService : IWalletService
{
    private readonly ApplicationDbContext _context;

    public WalletService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public WalletResponse ReplenishForAuthenticated(int userId,int walletId, double amount)
    {
        var wallet = _context.Wallet?.Find(walletId);
        var user = _context.Users?.Find(userId);
        if (wallet == null )
        {
            return new WalletResponse(false, "Wrong with server,try again later");
        }
        if (wallet.Balance + amount <= 100000 )
        {
            wallet.Balance += amount;
            var recharge = new Recharge
            {
                Recharger = user,
                Amount = amount,
                DateTime = DateTime.Now,
                IsIncome = true
            };


                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Entry(wallet).State = EntityState.Modified;
                        _context.SaveChanges();

                        _context.Recharges.Add(recharge);
                        _context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            
            
           
    
            
            return new WalletResponse(true, $"Wallet successfully replenished to {amount}");
        }

        return new WalletResponse(false, "You cant have balance over 100000");
        
    }

    public  WalletResponse ReplenishForNonAuthenticated(int userId,int walletId, double amount)
    {
        var wallet = _context.Wallet?.Find(walletId);
        var user = _context.Users?.Find(userId);
        if (wallet == null )
        {
            return new WalletResponse(false, "Wrong with server,try again later");
        }
        if (wallet.Balance + amount <= 10000)
        {
            wallet.Balance += amount;
            var recharge = new Recharge
            {
                Recharger = user,
                Amount = amount,
                DateTime = DateTime.Now,
                IsIncome = true
            };
            
            
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Entry(wallet).State = EntityState.Modified;
                    _context.SaveChanges();

                    _context.Recharges.Add(recharge);
                    _context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            
            
            return new WalletResponse(true, $"Wallet successfully replenished to {amount}");
        }

        return new WalletResponse(false, "You cant have balance over 10000");
    }

    public RechargeResponse GetMonthRecharge(int userId, int walletId, int month, int year)
    {
        var recharges = _context.Recharges.Where(e => e.Recharger.Id == userId  && e.DateTime.Year == year && e.DateTime.Month==month && e.IsIncome).ToArray();


        var rechargeResponse = new RechargeResponse
        {
            Recharges = recharges.ToList(),
            NumberOfRecharges = recharges.Length,
            Total = recharges.Select(a => a.Amount).Sum()
        };
        

        return rechargeResponse;
    }
}