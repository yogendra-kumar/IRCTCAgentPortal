using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Api.TokenProvider.Controllers
{
    [Route("Mpower/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private IMerchantRepository _merchantRepository;
        private readonly IApplicationErrorRepository _errorsRepository;
        //ApplicationDbContext _applicationDbContext = null;

        public AuthenticateController(IOptions<JwtIssuerOptions> jwtOptions,
                                        IMerchantRepository MerchantRepository,
                                        IApplicationErrorRepository ApplicationErrorsRepository,
                                        ILoggerFactory loggerFactory)
        {

            _errorsRepository = ApplicationErrorsRepository;
            _merchantRepository = MerchantRepository;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<AuthenticateController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromForm] Rail_Merchant merchant)
        {
            
            Rail_Merchant _merchant = _merchantRepository.GetSingle(a => a.UserName == merchant.UserName && a.Password == merchant.Password);
            
            var identity = await GetClaimsIdentity(_merchant);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({merchant.UserName}) or password ({merchant.Password})");
                return BadRequest("Invalid credentials");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, merchant.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("DisneyCharacter")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// IMAGINE BIG RED WARNING SIGNS HERE!
        /// You'd want to retrieve claims through your claims provider
        /// in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private static Task<ClaimsIdentity> GetClaimsIdentity(Rail_Merchant merchant)
        {
            //if (merchant.UserName == "MickeyMouse" && merchant.Password == "MickeyMouseIsBoss123")
            if (merchant!=null)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(merchant.UserName, "Token"),
                  new[]
                  {
                        new Claim("Mpower.Oxigen.Rail", "Agent")
                  }));
            }

            // if (merchant.UserName == "NotMickeyMouse" && merchant.Password == "NotMickeyMouseIsBoss123")
            // {
            //     return Task.FromResult(new ClaimsIdentity(new GenericIdentity(merchant.UserName, "Token"),
            //       new Claim[] { }));
            // }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}