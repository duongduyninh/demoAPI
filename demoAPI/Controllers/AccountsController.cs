using demoAPI.Data;
using demoAPI.Models;
using demoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly BookStoreContext _DBcontext;

        public AccountsController(IAccountRepository repo,
               BookStoreContext DBcontext) {

            accountRepository = repo;
            _DBcontext = DBcontext;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                var user = await _DBcontext.Users
                            .Where(u => u.Email == signUpModel.Email)
                            .Select(u => new
                            {
                                u.Id,
                                u.Email
                            })
                            .FirstOrDefaultAsync();
                return Ok(user); 
            }
            return StatusCode(500);
        }
        [HttpPost("SignIn")] 
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepository.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
