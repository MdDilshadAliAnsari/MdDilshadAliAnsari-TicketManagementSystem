using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TMS.Authentication.Authenticate;
using TMS.Authentication.Model;
using static System.Net.WebRequestMethods; 
using TMS.Authentication.Controllers;
using Azure.Core;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Azure;
using Microsoft.AspNetCore.Identity;

namespace TMS.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        #region  Variable Constructor and Dependency injection in Constructor level
        private readonly TMSDbContext _context;  
        private readonly IAuthenticationUserNew _tokenServiceNew;
        public LoginController(TMSDbContext context,  IAuthenticationUserNew tokenServiceNew)
        {
            _context = context; 
            _tokenServiceNew= tokenServiceNew ?? throw new ArgumentNullException( nameof(tokenServiceNew));
        }
        #endregion
        #region Login Related API 

        [HttpPost("signin")]
        public IActionResult Login([FromBody] user user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user request!");
            }

            var result = _context.USERS
                .SingleOrDefault(m => m.USERNAME == user.USERNAME && m.PWD == user.PWD);

            if (result == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var resultRole = _context.ROLES
                .SingleOrDefault(m => m.ROLEID == result.ROLEID);

            if (resultRole == null)
            {
                return Unauthorized("Role not found for the user");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.USERNAME),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, resultRole.ROLENAME) // Assuming ROLENAME is the role's name
            }; 
            var token = _tokenServiceNew.CreateTokenNew(authClaims); 
            // Convert the token into a compact JWT string
            var tokenHandler = new JwtSecurityTokenHandler();
            string jwtString = tokenHandler.WriteToken(token);




            var refreshToken = _tokenServiceNew.GenerateRefreshTokenNew();
       
            _ = int.TryParse(
                TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["JWT:JwtExpireMint"],
                out int refreshTokenValidityInMint);

            var Auth = _context.AuthToken.Where(x => x.USERNAME == user.USERNAME).FirstOrDefault();

            if (Auth == null)
            { 
                // Store refresh token logic here (e.g., save to DB)
                var obj = new AuthTokens
                {
                    AUTHTOKENID = null,
                    TOKEN = token.EncodedPayload.ToString(),
                    REFRESHTOKEN = refreshToken,
                    TOKENTYPE = "Access",
                    USERID = result.USERID,
                    USERNAME = result.USERNAME,
                    ISSUEDAT = DateTime.Now,
                    EXPIREDAT = DateTime.Now.AddMinutes(refreshTokenValidityInMint),
                    ISREVOKED = 0,
                    IPADDRESS = "::1",
                    USERAGENT = "Internal IIS",
                    ISDELETED = 0,
                    CREATEDBY = 1,
                    UPDATEDBY = 1,
                    CREATEDON = DateTime.Now,
                    UPDATEDON = DateTime.Now
                };

                _context.AuthToken.Add(obj);
                _context.SaveChanges();
            }
            else 
            {
                _context.Entry(Auth).State = EntityState.Detached; 
                var obj = new AuthTokens
                {
                    AUTHTOKENID = null,
                    TOKEN = token.EncodedPayload.ToString(),
                    REFRESHTOKEN = refreshToken,
                    TOKENTYPE = "Access",
                    USERID = result.USERID,
                    USERNAME = result.USERNAME,
                    ISSUEDAT = DateTime.Now,
                    EXPIREDAT = DateTime.Now.AddMinutes(refreshTokenValidityInMint),
                    ISREVOKED = 0,
                    IPADDRESS = "::1",
                    USERAGENT = "Internal IIS",
                    ISDELETED = 0,
                    CREATEDBY = 1,
                    UPDATEDBY = 1,
                    CREATEDON = DateTime.Now,
                    UPDATEDON = DateTime.Now
                };
                _context.AuthToken.Update(obj);
                _context.SaveChangesAsync();

            }



            return Ok(new
            {
                token = jwtString,
                refreshToken = refreshToken,
                expiresInMint = refreshTokenValidityInMint
            }); 

        } 

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenNew(TokenApiModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = _tokenServiceNew.GetPrincipalFromExpiredTokenNew(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string username = principal.Identity.Name;
            var user = await _context.AuthToken.Where(x => x.USERNAME == username).FirstOrDefaultAsync();

            if (user == null || user.REFRESHTOKEN != refreshToken || user.EXPIREDAT <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _tokenServiceNew.CreateTokenNew(principal.Claims.ToList());
            var newRefreshToken = _tokenServiceNew.GenerateRefreshTokenNew();

            user.REFRESHTOKEN = newRefreshToken;
            _context.Entry(user).State = EntityState.Detached;
            _context.AuthToken.Update(user);
            await _context.SaveChangesAsync(); 

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{UserName}")]
        public async Task<IActionResult> RevokeNew(string UserName)
        { 
            var authToken = await _context.AuthToken.Where(x => x.USERNAME == UserName).FirstOrDefaultAsync();
            if (authToken == null)
            { return BadRequest("Invalid user name"); }
            else
            {
                authToken.REFRESHTOKEN  = null;
                authToken.TOKEN         = null;
                _context.Entry(authToken).State = EntityState.Detached;
                _context.AuthToken.Update(authToken);
                await _context.SaveChangesAsync(); 
                return NoContent();
            }

           
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAllNew()
        {
            var Auth = _context.AuthToken.ToList();
            foreach (var user in Auth)
            {
                user.REFRESHTOKEN = null;
                user.TOKEN = null;
                _context.Entry(user).State = EntityState.Detached; 
                _context.AuthToken.Update(user);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }















 

        #endregion
        #region Change password
        [HttpPost("ChangePwd")]
        [Authorize]
        public IActionResult ChangePwd([FromBody] user user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            var result = _context.USERS.Where(m => m.USERNAME == user.USERNAME).SingleOrDefault();


            if (result != null)
            {
                var resultRole = _context.ROLES
                             .Where(m => m.ROLEID == result.ROLEID)
                             .SingleOrDefault();


                if (result != null)
                {  result.PWD = user.PWD; 
                   _context.SaveChanges(); // Apply the update to the database                    
                }
                var userData = new
                {
                    result.USERNAME,
                    result.PWD,
                    result.EMAILID,
                    result.MOBILENO
                };
                var response = new { userData };
                return Ok(new { response });
            }
            return Unauthorized();



        }

        #endregion
        #region Forget password
        [HttpPost("ForgetPwd")] 
        public IActionResult ForgetPwd([FromBody] user user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            var result = _context.USERS.Where(m => m.USERNAME == user.USERNAME).SingleOrDefault();


            if (result != null)
            {
                var resultRole = _context.ROLES
                             .Where(m => m.ROLEID == result.ROLEID)
                             .SingleOrDefault(); 
                 
                var userData = new
                {
                    result.USERNAME,
                    result.PWD,
                    result.EMAILID,
                    result.MOBILENO
                };
                var response = new { userData };
                return Ok(new { response });
            }
            return Unauthorized();



        }

        #endregion
        #region Register a user 
        [HttpPost("Register")]
        public IActionResult Register([FromBody] user user)
        {
            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            var result = _context.USERS.Where(m => m.MOBILENO == user.MOBILENO && m.EMAILID == user.EMAILID).SingleOrDefault();
            if (result != null)
            {
                string msg = "Email Or Mobile No Already Exist!!!";
                var response = new { msg };
                return Ok(new { response });
            }
            else
            {

                if (ModelState.IsValid)
                {
                    user.USERID = null;
                    user.ISDELETED = 0;
                    user.CREATEDBY = 1;
                    user.UPDATEDBY = 1;
                    user.CREATEDON = DateTime.Now;
                    user.UPDATEDON = DateTime.Now;                   
                    _context.USERS.Add(user);
                    _context.SaveChanges();

                    var userData = new
                    {
                        user.USERNAME,

                    };
                    var response = new { userData };
                    return Ok(new { response });
                }
                return Unauthorized();
            }

        }



        #endregion
        #region Getting All Role
        [HttpGet("TASKSTATUS")]
        public IActionResult ROLELIST()
        {

            var result = _context.ROLES.Select(e => new { e.ROLENAME, e.ROLEID }).ToListAsync();
            if (result == null)
            {
                return NotFound("records not found!!!");
            }
            var response = new { result };
            return Ok(new { response }); 
        }
        #endregion





    }
}
 
