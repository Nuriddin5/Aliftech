using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test25_08.DTOs;
using test25_08.Models;
using test25_08.Service;

namespace test25_08.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWalletService _walletService;

        public WalletController(ApplicationDbContext context, IWalletService walletService)
        {
            _context = context;
            _walletService = walletService;
        }

        [HttpGet("checkWalletAccountExists{walletId}")]
        public ActionResult<WalletResponse> CheckWalletAccountExists(int walletId)
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            if (!WalletExists(walletId))
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost("replenishWallet")]
        public ActionResult<WalletResponse> ReplenishWallet(ReplenishWalletDto replenishWalletDto)
        {
            if (replenishWalletDto.Amount <= 0)
            {
                return BadRequest("You should enter valid amount");
            }
            var currentUser = _context.Users?.Find(replenishWalletDto.UserId);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (_context.Wallet == null)
            {
                return Problem();
            }

            try
            {
                var wallet = _context.Wallet.First(w => w.Owner != null && w.Owner.Id == replenishWalletDto.UserId && w.Id == replenishWalletDto.WalletId);
            }
            catch (Exception e)
            {
                return BadRequest("You not owner of this wallet");
            }

            if (currentUser.IsAuthenticated)
            {
                return _walletService.ReplenishForAuthenticated(replenishWalletDto.UserId, replenishWalletDto.WalletId, replenishWalletDto.Amount);
            }

            return _walletService.ReplenishForNonAuthenticated(replenishWalletDto.UserId, replenishWalletDto.WalletId, replenishWalletDto.Amount);
        }

        [HttpPost("GetMonthRecharge")]
        public ActionResult<RechargeResponse> GetMonthRecharge(MonthRechargeDto monthRechargeDto)
        {
            if (monthRechargeDto.Year <= 0 ||  DateTime.Now.Year < monthRechargeDto.Year || DateTime.Now.Month < monthRechargeDto.Month || monthRechargeDto.Month <= 0)
            {
             return   BadRequest("Enter valid year or month");
            }  
            
            
            var currentUser = _context.Users?.Find(monthRechargeDto.UserId);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (_context.Wallet == null)
            {
                return Problem();
            }

            try
            {
                var wallet = _context.Wallet.First(w => w.Owner != null && w.Owner.Id == monthRechargeDto.UserId && w.Id == monthRechargeDto.WalletId);
            }
            catch (Exception e)
            {
                return BadRequest("You not owner of this wallet");
            }

            return Ok(_walletService.GetMonthRecharge(monthRechargeDto.UserId, monthRechargeDto.WalletId, monthRechargeDto.Month, monthRechargeDto.Year));
        }

        [HttpGet("getWalletBalance{walletId}")]
        public ActionResult<WalletResponse> GetWalletBalance(int walletId)
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            if (!WalletExists(walletId))
            {
                return NotFound(new WalletResponse(false, $"Wallet with {walletId} id not found"));
            }

            var wallet = _context.Wallet.Find(walletId);
            if (wallet != null) return new WalletResponse(true, wallet.Balance);
            return Problem("That's wrong please try soon");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallet()
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            return await _context.Wallet.ToListAsync();
        }

        // GET: api/v1/Wallet/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(int id)
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        // PUT: api/v1/Wallet/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWallet(int id, Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Wallet
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet wallet, int ownerId)
        {
            if (_context.Wallet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wallet'  is null.");
            }

            if (_context.Users != null)
                wallet.Owner = await _context.Users.FindAsync(ownerId);

            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        // DELETE: api/Wallet/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallet.Remove(wallet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WalletExists(int id)
        {
            return (_context.Wallet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}