﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test25_08.Models;
using test25_08.Service;

namespace test25_08.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
        public ActionResult<WalletExistsResponse> CheckWalletAccountExists(int walletId)
        {
            if (_context.Wallet == null)
            {
                return NotFound();
            }

            if (!WalletExists(walletId))
            {
                return new WalletExistsResponse(false, $"Wallet with {walletId} id not found");
            }
            else
            {
                return new WalletExistsResponse(true, $"Wallet with {walletId} id is exists");
            }
        }

        [HttpPost("replenishWallet")]
        public ActionResult<ReplenishResponse> ReplenishWallet(int walletId, double amount)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetMonthRecharge{month}/{year}")]
        public ActionResult<double> GetMonthRecharge(int month, int year)
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