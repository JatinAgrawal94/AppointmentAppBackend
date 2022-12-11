using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using AppointmentAppBackend.Model;
using AppointmentAppBackend.Dtos;
using AppointmentAppBackend.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace AppointmentAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<HospitalUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<HospitalUser> userManager, ApplicationDbContext context, JwtHandler jwtHandler, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _context = context;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
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
                Token = jwt,
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
                    var roles= await _userManager.GetRolesAsync(result);
                    await _userManager.DeleteAsync(result);
                    await _userManager.RemoveFromRolesAsync(result,roles);
                    Console.WriteLine(result.UserName);
                    return Ok("Deleted");
                }
            } catch (Exception e) {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(RegisterUser registeruser,String role) 
        {
            HospitalUser user=await _userManager.FindByNameAsync(registeruser.UserName);
            if(user==null)
            {
                if((registeruser.Password!=null && registeruser.ConfirmPassword != null) && (registeruser.Password == registeruser.ConfirmPassword))
                {
                    var newUser = new HospitalUser { UserName=registeruser.UserName };
                    IdentityResult result=await _userManager.CreateAsync(newUser, registeruser.Password);
                    //user=await _userManager.FindByNameAsync(registeruser.UserName);
                   // IdentityResult roles = await _userManager.AddToRoleAsync(user,role);
                    if (result.Succeeded)
                    {
                        return Ok(new { 
                            Success=true
                        });
                    }
                    else
                    {
                        return BadRequest(new{ message=result});
                    }
                }
            }
                return StatusCode(403);
        }
/*
        [HttpPost("role/assignrole")]
        public async Task<IActionResult> AssignRole(String username,String role)
        {
            HospitalUser user = await _userManager.FindByNameAsync(username);
            if(user== null){
                return NotFound();
            }
            else{                  
            
                IdentityResult roles= await _userManager.AddToRoleAsync(user,role);
                if (roles.Succeeded)
                {
                    return Ok(new
                    {
                        Success=true,
                    });
                }
                else
                {
                    return BadRequest(new{ message=roles});
                }
            }
        }*/
    }
}