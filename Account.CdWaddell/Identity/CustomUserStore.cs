// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) C Daniel Waddell. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Account.CdWaddell.Data;

namespace Account.CdWaddell.Identity
{
    internal class CustomUserStore:UserStore<CustomUser, CustomRole, CdWaddellContext, string>
    {
        public CustomUserStore(CdWaddellContext context, IdentityErrorDescriber describer = null) : 
            base(context, describer)
        {
        }
    }
}