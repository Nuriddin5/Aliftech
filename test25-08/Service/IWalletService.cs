using Microsoft.AspNetCore.Mvc;
using test25_08.DTOs;
using test25_08.Models;

namespace test25_08.Service;

public interface IWalletService
{
    WalletResponse ReplenishForAuthenticated(int userId,int walletId, double amount);
    WalletResponse ReplenishForNonAuthenticated(int userId,int walletId, double amount);
    RechargeResponse GetMonthRecharge(int userId, int walletId, int month, int year);
}