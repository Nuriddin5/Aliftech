// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Primitives;
// using Microsoft.IdentityModel.Tokens;
// using test25_08.Models;
//
// namespace test25_08.Controllers;
//
// [ApiController]
// [Route("api/v1/accounts")]
// public class AccountsController : ControllerBase
// {
//     private readonly UserManager<User> _userManager;
//     private readonly SignInManager<User> _signInManager;
//     private readonly IConfiguration _configuration;
//
//
//     public AccountsController(UserManager<User> userManager,
//         SignInManager<User> signInManager, IConfiguration configuration)
//     {
//         _userManager = userManager;
//         _signInManager = signInManager;
//         _configuration = configuration;
//     }
//
//     [HttpPost("create")]
//     public async Task<ActionResult<AuthentificationResponse>> Create([FromBody] UserCredentials userCredentials)
//     {
//         var user = new User
//         {
//             UserName = userCredentials.Email, Email = userCredentials.Email
//         };
//         // Request.Headers.Add("X-UserId", user.Id);
//         // Request.Headers.Add("X-Digest", HashSumOfBody(Request.Body));
//
//         var identityResult = await _userManager.CreateAsync(user, userCredentials.Password);
//         if (identityResult.Succeeded)
//         {
//             return BuildToken(userCredentials);
//         }
//         else
//         {
//             return BadRequest(identityResult.Errors);
//         }
//     }
//
//     [HttpPost("login")]
//     public async Task<ActionResult<AuthentificationResponse>> Login([FromBody] UserCredentials userCredentials)
//     {
//         var identityResult = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password,
//             isPersistent: false, lockoutOnFailure: false);
//         if (identityResult.Succeeded)
//         {
//             return BuildToken(userCredentials);
//         }
//         else
//         {
//             return BadRequest("Incorrect Login");
//         }
//     }
//
//     private StringValues HashSumOfBody(Stream requestBody)
//     {
//         throw new NotImplementedException();
//     }
//
//     private AuthentificationResponse BuildToken(UserCredentials userCredentials)
//     {
//         var claims = new List<Claim>()
//         {
//             new Claim("email", userCredentials.Email)
//         };
//         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key"]));
//
//         var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//
//         var expiration = DateTime.UtcNow.AddYears(1);
//         var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
//             expires: expiration, signingCredentials: credentials);
//
//
//         return new AuthentificationResponse()
//         {
//             Token = new JwtSecurityTokenHandler().WriteToken(token)
//         };
//     }
// }