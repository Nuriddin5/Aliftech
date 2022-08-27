using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test25_08;
using test25_08.Models;

namespace test25_08.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("checkWalletAccountExists{walletId}")]
        public ActionResult<bool> CheckWalletAccountExists(int walletId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("replenishWallet")]
        public ActionResult<ReplenishResponse> ReplenishWallet(int walletId, double amount)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetMonthRecharge{month}/{year}")]
        public ActionResult<double> GetMonthRecharge(int date,int year)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getWalletBalance{walletId}")]
        public ActionResult<double> GetWalletBalance(int walletId)
        {
            throw new NotImplementedException();
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

        // GET: api/Wallet/5
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

        // PUT: api/Wallet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet wallet)
        {
            if (_context.Wallet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wallet'  is null.");
            }

            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        // DELETE: api/Wallet/5
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