// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) C Daniel Waddell. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Account.CdWaddell.Data;

namespace Account.CdWaddell.Identity
{
    internal class CustomSignInManager:SignInManager<CustomUser>
    {
        public CustomSignInManager(UserManager<CustomUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<CustomUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<CustomUser>> logger, IAuthenticationSchemeProvider schemes) : 
            base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}