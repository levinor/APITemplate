using Levinor.Business.Domain;
using Levinor.Business.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Levinor.Business.Services
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache,
            ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Guid generateUserToken(User user)
        {
            Guid token = Guid.NewGuid();

            //Security Tokens may expire in an hour
            _cache.Set(token, user, 
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

            _logger.LogDebug($"New user token for userId {user.Id}: {token}");
            return token;
        }

        public bool checkAuthToken(Guid token, out User user)
        {
           if(_cache.TryGetValue<User>(token, out user))
           {
                //Renew cache entry and return true and user
                _cache.Remove(token);
                _cache.Set(token, user,
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));

                return true;
            }
            else
            {
                throw new ArgumentException("Invalid Token. Login again");
            }
        }
    }
}
