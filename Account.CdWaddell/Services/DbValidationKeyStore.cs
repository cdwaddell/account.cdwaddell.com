using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Account.CdWaddell.Data;
using IdentityServer4.Stores;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Account.CdWaddell.Services
{
    public class DbValidationKeyStore : IValidationKeysStore, ISigningCredentialStore
    {
        private readonly CdWaddellContext _context;
        private readonly IMemoryCache _cache;

        public DbValidationKeyStore(CdWaddellContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public Task<IEnumerable<SecurityKey>> GetValidationKeysAsync()
        {
            return Task.FromResult(_cache.GetOrCreate("ValidationKeys", entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(20);

                var keys = _context.IdentityCertificates
                    .Where(c => c.Active)
                    .Select(c => new X509SecurityKey(new X509Certificate2(c.FileData)))
                    .Cast<SecurityKey>()
                    .ToList();

                return keys.AsEnumerable();
            }));
        }

        public Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            return Task.FromResult(_cache.GetOrCreate("SigningKey", entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(20);

                var primary = _context.IdentityCertificates
                    .Where(c => c.Active && c.Primary)
                    .OrderByDescending(c => c.Id)
                    .AsEnumerable()
                    .Select(c => new X509SecurityKey(new X509Certificate2(c.FileData)))
                    .FirstOrDefault();

                return new SigningCredentials(primary, SecurityAlgorithms.RsaSha256);

            }));
        }
    }
}