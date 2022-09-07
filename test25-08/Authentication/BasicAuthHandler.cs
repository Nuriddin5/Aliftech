using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Request.Body.Peeker;

namespace test25_08.Authentication;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ApplicationDbContext _context;

    public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, ApplicationDbContext context) : base(options, logger, encoder, clock)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string method = Request.Method;

        if (!Request.Headers.ContainsKey("X-UserId"))
        {
            return AuthenticateResult.Fail("No header found");
        }


        var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["X-UserId"]);

        if (headerValue.Parameter != null)
        {
            var bytes = Convert.FromBase64String(headerValue.Parameter);

            var credentials = Encoding.UTF8.GetString(bytes);


            if (!string.IsNullOrEmpty(credentials))
            {
                var strings = credentials.Split(":");

                var userId = strings[0];

                var password = strings[1];

                if (_context.Users != null)
                {
                    var user = _context.Users
                        .FirstOrDefault(item =>
                            item.Id == Convert.ToInt32(userId) && item.Password == password
                        );

                    if (user == null)
                    {
                        return AuthenticateResult.Fail("No user found");
                    }
                }

                if (method.Equals("POST"))
                {
                    string secret = "secret";
                    var headerValue2 = AuthenticationHeaderValue.Parse(Request.Headers["X-Digest"]);

                    if (!Request.Headers.ContainsKey("X-Digest"))
                    {
                        return AuthenticateResult.Fail("No header found");
                    }

                    var s = await Request.PeekBodyAsync();

                    string replace = s.Replace("\r\n", "\n");
                    // string replace = Regex.Replace(s, @"\s+", "");
                    // string replace = s.Replace("\r,","");
                    if (!headerValue2.Parameter!.Equals(HashString(replace, secret)))
                    {
                        return AuthenticateResult.Fail("Request body doesn't match");
                    }
                }

                var claim = new[] { new Claim(ClaimTypes.Name, userId) };
                var identity = new ClaimsIdentity(claim, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);


                return AuthenticateResult.Success(ticket);
            }
        }

        return AuthenticateResult.Fail("UnAuthorized");
    }


    static string HashString(string stringToHash, string hachKey)
    {
        UTF8Encoding myEncoder = new UTF8Encoding();
        byte[] key = myEncoder.GetBytes(hachKey);
        byte[] text = myEncoder.GetBytes(stringToHash);
        HMACSHA1 myHmacsha1 = new HMACSHA1(key);
        byte[] hashCode = myHmacsha1.ComputeHash(text);
        string hash = BitConverter.ToString(hashCode).Replace("-", "");
        return hash.ToLower();
    }
}