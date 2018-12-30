using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Task3.Models;
using Task3.UserData;

namespace Task3.Controllers
{
    [ApiController, Route("api/tokens")]
    public class TokenController : ControllerBase
    {
        private IUserRepository userRepository = new UserRepository();
        private IConfiguration Configuration;
        private SecurityKey tokenSymmetricKey;

        public TokenController(IConfiguration configuration)
        {
            Configuration = configuration;
            tokenSymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["tokenSymmetricKey"]));
        }

        #region Token Controllers
        //1. Get User Token
        [HttpGet, AllowAnonymous]
        public IActionResult RequestToken()
        {
            string username, password, authHeader, loginCredentialsRaw;
            string[] loginCredentials;
            User returnedUser;
            
            authHeader = Request.Headers["Authorization"].ToString();
            
            //If request is not using Basic Authentication Header
            if (!authHeader.StartsWith("Basic"))
            {
                return BadRequest("Invalid request parameters");
            }
            
            try
            {
                loginCredentialsRaw = authHeader.Substring("Basic ".Length).Trim();
                loginCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(loginCredentialsRaw)).Split(":");
                username = loginCredentials[0];
                password = loginCredentials[1];
            }
            catch (Exception) { return StatusCode(500); }
            
            returnedUser = userRepository.Get(username, password);
            if (returnedUser == null)
            {
                return BadRequest("Invalid username or password");
            }
            
            try
            {
                string token = this.generateToken(returnedUser);
                Console.WriteLine(token);
                return Ok(token);
            }
            catch (Exception) { return StatusCode(500); }
        }

        //2. Check Token Validity
        [HttpGet, Route("checkToken"), Authorize]
        public IActionResult CheckToken()
        {
            return Ok("Token is valid.");
        }

        //3. Check Admin Role
        [HttpGet, Route("checkAdmin"), Authorize(Policy = "AdminOnly")]
        public IActionResult CheckAdmin()
        {
            return Ok("Token has administrative powers.");
        }

        //4. Get Token Info
        [HttpGet, Route("getTokenInfo"), Authorize]
        public IEnumerable GetTokenInfo()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            return decodeToken(token);
        }
        #endregion

        #region Token Generator
        private string generateToken(User inputUser)
        {
            List<Claim> claimDataRaw = new List<Claim>();
            claimDataRaw.Add(new Claim(ClaimTypes.Name, inputUser.Username));
            claimDataRaw.Add(new Claim(ClaimTypes.Email, inputUser.Email));
            claimDataRaw.Add(new Claim("Id", inputUser.Id.ToString()));
            claimDataRaw.Add(new Claim("First Name", inputUser.FirstName));
            claimDataRaw.Add(new Claim("Last Name", inputUser.LastName));
            claimDataRaw.Add(new Claim("Full Name", inputUser.FullName));
            claimDataRaw.Add(new Claim("Address", inputUser.Address));
            if (inputUser.IsAdmin) claimDataRaw.Add(new Claim("IsAdmin", "Yes"));

            Claim[] claimData = claimDataRaw.ToArray();

            var tokenRaw = new JwtSecurityToken(
                issuer: Configuration["tokenIssuer"],
                audience: Configuration["tokenAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: claimData,
                signingCredentials: new SigningCredentials(tokenSymmetricKey, SecurityAlgorithms.HmacSha256Signature)
            );
            
            string token = new JwtSecurityTokenHandler().WriteToken(tokenRaw);
            return token;
            
        }

        private string[] decodeToken(string inputToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = (JwtSecurityToken)handler.ReadToken(inputToken);
            IEnumerable<Claim> tokenClaims = token.Claims;

            List<string> tokenInfo = new List<string>();
            foreach (Claim tokenClaim in tokenClaims)
            {
                tokenInfo.Add(tokenClaim.Type + ": " + tokenClaim.Value);
            }
            return tokenInfo.ToArray();
        }
        #endregion
    }
}
