using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EFXServices.ApplicationModels;
using EFXServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EFXServices.Controllers
{


    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        private IConfiguration configuration;
        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await userManager.FindByNameAsync(model.Username);

            if ((user != null) && (await userManager.CheckPasswordAsync(user, model.Password)))
            {


                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    
                };

                // Add roles to token

                var roles = await userManager.GetRolesAsync(user);

                claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));


                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenItems:SigningKey"]));

                int expireMinutes = Convert.ToInt32(configuration["TokenItems:ExpireMinutes"]);

                var token = new JwtSecurityToken(
                    issuer: configuration["TokenItems:URL"],
                    audience: configuration["TokenItems:URL"],
                    expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                    claims: claims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }

                );

            }
            return Unauthorized();
        }
    }
}