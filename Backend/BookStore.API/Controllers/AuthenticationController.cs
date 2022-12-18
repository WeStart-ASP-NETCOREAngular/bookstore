using BookStore.API.DTOs;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtToken;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationController(IJwtTokenGenerator jwtToken, UserManager<AppUser> userManager, IMapper mapper)
        {
            _jwtToken = jwtToken;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.email);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.password))
            {
                if (user.IsActive)
                {
                    var token = await _jwtToken.Generate(user);
                    return Ok(token);
                }
                else
                    return Unauthorized(new { message = "unactive account, please contact administrator" });

            }
            else
                return Unauthorized(new { message = "Unauthorized" });
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {

            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
            {
                return BadRequest("User exists!");
            }

            var identityUser = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(new { Errors = errors });
            }

            await _userManager.AddToRoleAsync(identityUser, "User");

            var token = await _jwtToken.Generate(identityUser);

            return Ok(token);


        }



    }
}
