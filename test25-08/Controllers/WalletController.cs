using Microsoft.AspNetCore.Mvc;
using test25_08.Models;

namespace test25_08.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public WalletController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public ActionResult<List<Wallet>> GetAll()
    {
        if (context.Wallet == null)
        {
            return NotFound();
        }

        var wallets = context.Wallet.ToList();
        Console.WriteLine(wallets);

        // context
        return wallets;
    }
}