using demoAPI.Controllers;
using demoAPI.Data;
using demoAPI.Helpers;
using demoAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demoAPI.Repositories.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IConfiguration configuration,
                                 RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;

        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var checkPassword = await userManager.CheckPasswordAsync(user, model.Passsword);

            if (user == null || !checkPassword)
            {
                return string.Empty;
            }
            else
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRole = await userManager.GetRolesAsync(user);
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                var issuer = configuration["JWT:ValidIssuer"];
                var audience = configuration["JWT:ValidAudience"];
                var expires = DateTime.Now.AddMinutes(20);
                var authenkey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    expires: expires,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenkey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }


        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var Result = await userManager.CreateAsync(user, model.Passsword);

            if (Result.Succeeded)
            {
                /* khởi tạo role trước khi thêm role vào user ( tránh thêm dữ liệu ) */
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }

                await userManager.AddToRoleAsync(user, AppRole.Customer);

            }

            return Result;
        }
    }
}
