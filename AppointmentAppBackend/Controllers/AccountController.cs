using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using AppointmentAppBackend.Model;
using AppointmentAppBackend.Dtos;
using AppointmentAppBackend.Data;

namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<HospitalUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<HospitalUser> userManager, ApplicationDbContext context, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            HospitalUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid Username or Password."
                });
            }
            JwtSecurityToken secToken = await _jwtHandler.GetTokenAsync(user);
            string? jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                Token = jwt
            });
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> DeleteUser(String user) {
            try {
                HospitalUser result=await _userManager.FindByNameAsync(user);
               
                if (result == null)
                {
                    return NotFound();
                }else
                {
                    await _userManager.DeleteAsync(result);
                    Console.WriteLine(result.UserName);
                    return Ok("Deleted");
                }
            } catch (Exception e) {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(RegisterUser registeruser) 
        {
            HospitalUser user=await _userManager.FindByNameAsync(registeruser.UserName);
            Console.WriteLine(user);
            if(user==null)
            {
                if((registeruser.Password!=null && registeruser.ConfirmPassword != null) && (registeruser.Password == registeruser.ConfirmPassword))
                {
                    var newUser = new HospitalUser { UserName=registeruser.UserName };
                    Console.WriteLine(newUser);
                    var result=await _userManager.CreateAsync(newUser, registeruser.Password);
                    return Ok(new { 
                        Success=true
                    });
                }
            }else
            {
                return StatusCode(403);
            }
            Console.WriteLine(user);
            return StatusCode(404);
        }
    }
}