using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using test25_08.Models;

namespace test25_08.Controllers
{
    
    // public class ClaimRequirementAttribute : TypeFilterAttribute
    // {
    //     public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    //     {
    //         Arguments = new object[] {new Claim(claimType, claimValue) };
    //     }
    // }
    //
    // public class ClaimRequirementFilter : IAuthorizationFilter
    // {
    //     readonly Claim _claim;
    //
    //     public ClaimRequirementFilter(Claim claim)
    //     {
    //         _claim = claim;
    //     }
    //
    //     public void OnAuthorization(AuthorizationFilterContext context)
    //     {
    //         var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
    //         if (!hasClaim)
    //         {
    //             context.Result = new ForbidResult();
    //         }
    //     }
    // }
    
    
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: api/Users
        [HttpGet]
        // [ClaimRequirement(MyClaimTypes.Permission, "CanReadResource")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  ActionResult<User> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
          }
          _context.Users.Add(user);
          _context.SaveChanges();

          return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
