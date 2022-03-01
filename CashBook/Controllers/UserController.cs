using CashBook.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                User checkUser = await _userManager.FindByEmailAsync(user.Email);
                if (checkUser != null) 
                    throw new InvalidOperationException($"user with useremail {user.Email} already exists");

                var result = await _userManager.CreateAsync(user, user.Password);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("could not create new user");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost("createToken")]
        public async Task<IActionResult> CreateToken([FromBody] User user)
        {
            try
            {
                var userCheck = await _userManager.FindByEmailAsync(user.Email);
                if (userCheck == null)
                    throw new InvalidOperationException("user doesn't exists");
                var result = await _signInManager.CheckPasswordSignInAsync(userCheck, user.Password,false);
                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      _config["Tokens:Issuer"],
                      _config["Tokens:Audience"],
                      claims,
                      expires: DateTime.UtcNow.AddMinutes(30),
                      signingCredentials: creds);

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Created("", results);

                }
                else
                {
                    return BadRequest(result);
                }
                

            }catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
