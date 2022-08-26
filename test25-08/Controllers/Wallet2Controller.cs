// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using NuGet.Versioning;
// using test25_08;
// using test25_08.Models;
//
//
// [Route("api/[controller]")]
// [ApiController]
// public class Wallet2Controller : ControllerBase
// {
//     private readonly ApplicationDbContext _context;
//
//     public Wallet2Controller(ApplicationDbContext context)
//     {
//         _context = context;
//     }
//     
//     
//     
//
//     // GET: api/Wallet2
//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<Wallet2>>> GetWallet2()
//     {
//         if (_context.Wallet2 == null)
//         {
//             return NotFound();
//         }
//
//         return await _context.Wallet2.ToListAsync();
//     }
//
//     // GET: api/Wallet2/5
//     [HttpGet("{id}")]
//     public async Task<ActionResult<Wallet2>> GetWallet2(int id)
//     {
//         if (_context.Wallet2 == null)
//         {
//             return NotFound();
//         }
//
//         var wallet2 = await _context.Wallet2.FindAsync(id);
//
//         if (wallet2 == null)
//         {
//             return NotFound();
//         }
//
//         return wallet2;
//     }
//
//     // PUT: api/Wallet2/5
//     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//     [HttpPut("{id}")]
//     public async Task<IActionResult> PutWallet2(string id, WalletDTO walletDTO)
//     {
//         var wallet = _context.Wallet2?.Find(id);
//         if (wallet != null)
//         {   
//             wallet.Balance = walletDTO.Balance;
//             
//                 wallet.OwnerId = walletDTO.OwnerId;
//                 wallet.Owner = _context.Users?.Find(walletDTO.OwnerId);
//             
//
//             _context.Entry(wallet).State = EntityState.Modified;
//         }
//         else
//         {
//             return NotFound();
//         }
//
//
//         try
//         {
//             await _context.SaveChangesAsync();
//         }
//         catch (DbUpdateConcurrencyException)
//         {
//             if (!Wallet2Exists(id))
//             {
//                 return NotFound();
//             }
//             else
//             {
//                 throw;
//             }
//         }
//
//         return NoContent();
//     }
//
//     // POST: api/Wallet2
//     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//     [HttpPost]
//     public async Task<ActionResult<Wallet2>> PostWallet2(WalletDTO walletDTO)
//     {
//         if (_context.Wallet2 == null)
//         {
//             return Problem("Entity set 'ApplicationDbContext.Wallet2'  is null.");
//         }
//
//         var wallet2 = new Wallet2();
//         // wallet2.Id = walletDTO.Id;
//         wallet2.OwnerId = walletDTO.OwnerId;
//         wallet2.Balance = walletDTO.Balance;
//         // wallet2.Owner = _context.Users.Where(u => u.Id == walletDTO.OwnerId).FirstOrDefault();
//
//
//         _context.Wallet2.Add(wallet2);
//         await _context.SaveChangesAsync();
//
//         // return CreatedAtAction("GetWallet2", new { id = wallet2.Id }, wallet2);
//         return Ok(wallet2);
//     }
//
//     // DELETE: api/Wallet2/5
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteWallet2(int id)
//     {
//         if (_context.Wallet2 == null)
//         {
//             return NotFound();
//         }
//
//         var wallet2 = await _context.Wallet2.FindAsync(id);
//         if (wallet2 == null)
//         {
//             return NotFound();
//         }
//
//         _context.Wallet2.Remove(wallet2);
//         await _context.SaveChangesAsync();
//
//         return NoContent();
//     }
//
//     private bool Wallet2Exists(int id)
//     {
//         return (_context.Wallet2?.Any(e => e.Id == id)).GetValueOrDefault();
//     }
// }