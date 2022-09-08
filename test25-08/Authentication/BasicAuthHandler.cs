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

        //You can(should) calculate X-UserId for request in https://www.base64encode.org/
        //!!! X-UserId in form  User.Id:User.Password
        
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

                //You can(should) calculate X-Digest for request body in
                //https://codebeautify.org/hmac-generator
                if (method.Equals("POST"))
                {
                    string secret = "secret";
                    var headerValue2 = AuthenticationHeaderValue.Parse(Request.Headers["X-Digest"]);

                    if (!Request.Headers.ContainsKey("X-Digest"))
                    {
                        return AuthenticateResult.Fail("No header found");
                    }

                    var requestBody = await Request.PeekBodyAsync();

                    string normalizedBody = requestBody.Replace("\r\n", "\n");
                    if (!headerValue2.Parameter!.Equals(HashString(normalizedBody, secret)))
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


    static string HashString(string stringToHash, string key)
    {
        UTF8Encoding myEncoder = new UTF8Encoding();
        byte[] keyBytes = myEncoder.GetBytes(key);
        byte[] text = myEncoder.GetBytes(stringToHash);
        HMACSHA1 hmacSha1 = new HMACSHA1(keyBytes);
        byte[] hashCode = hmacSha1.ComputeHash(text);
        string hash = BitConverter.ToString(hashCode).Replace("-", "");
        return hash.ToLower();
    }
}