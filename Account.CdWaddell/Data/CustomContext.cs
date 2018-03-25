// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) C Daniel Waddell. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Account.CdWaddell.Services;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Account.CdWaddell.Data
{
    public class CdWaddellContext: 
        IdentityDbContext<CustomUser, CustomRole, string>, 
        IConfigurationDbContext, IPersistedGrantDbContext
    {
        private readonly ConfigurationStoreOptions _configStoreOptions;
        private readonly OperationalStoreOptions _operStoreOptions;

        public CdWaddellContext(DbContextOptions<CdWaddellContext> dbOptions, ConfigurationStoreOptions configStoreOptions, OperationalStoreOptions operStoreOptions) 
            : base(dbOptions)
        {
            _configStoreOptions = configStoreOptions;
            _operStoreOptions = operStoreOptions;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(SaveChanges());
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<IdentityCertificate> IdentityCertificates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityCertificate>(t =>
            {
                t.ToTable("IdentityCertificates");
            });

            builder.Entity<DataProtectionKey>(t =>
            {
                t.Property(x => x.FriendlyName)
                    .HasMaxLength(1024);

                t.ToTable("DataProtectionKeys");
            });

            builder.ConfigureClientContext(_configStoreOptions);
            builder.ConfigureResourcesContext(_configStoreOptions);
            builder.ConfigurePersistedGrantContext(_operStoreOptions);
        }
    }
}