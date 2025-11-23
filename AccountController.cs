using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestProject.Context;
using TestProject.DTOS;

namespace TestProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<NewUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<NewUser> userManager,IConfiguration config)
        {
            this._userManager = userManager;
            this._config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Signin(Sign_DTO NewUser)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!(NewUser.Role == "user" || NewUser.Role == "admin"))
            {

                return BadRequest("role not vaild");


            }

            var user = new NewUser
            {
                Email = NewUser.Email,

                UserName = NewUser.Name
            };

          var result=  await _userManager.CreateAsync(user,NewUser.Password);
           await _userManager.AddToRoleAsync(user,NewUser.Role);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                    return BadRequest(ModelState);
                }
            }

            return Ok("create");
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login_Dto logUser)
        {


            if (!ModelState.IsValid)
            { 
                
                return BadRequest(ModelState); 
            
            }


            var user = await _userManager.FindByNameAsync(logUser.Name);
            if (user is null)
                
                return Unauthorized();

            var passwordOk = await _userManager.CheckPasswordAsync(user, logUser.Password);
            if (!passwordOk)
                
                return Unauthorized();
            var roles=await _userManager.GetRolesAsync(user);
            StringBuilder roleAsString = new StringBuilder();
            


            foreach (var item in roles)
            {
                roleAsString.Append(item);

            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(ClaimTypes.Role,roleAsString.ToString()  ),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds
            );





            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString, expires = token.ValidTo });



        }

    }
}
