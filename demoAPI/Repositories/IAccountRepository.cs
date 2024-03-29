﻿using demoAPI.Controllers;
using demoAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace demoAPI.Repositories
{
        public interface IAccountRepository
        {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);

         }
}
