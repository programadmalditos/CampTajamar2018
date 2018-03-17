using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ApiCampTajamar.Options
{
    public class JwtOptions
    {
            public string Issuer { get; set; }

            public string Subject { get; set; }

            public string Audience { get; set; }

            public DateTime NotBefore { get; set; } = DateTime.UtcNow;

            public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

            public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);

            public DateTime Expiration { get; set; }

            public Func<Task<string>> JtiGenerator =>

                () => Task.FromResult(Guid.NewGuid().ToString());

            public SigningCredentials SigningCredentials { get; set; }

            public void UpdateToken()

            {

                IssuedAt = DateTime.UtcNow;

                NotBefore = IssuedAt;

                Expiration = IssuedAt.Add(ValidFor);

            }

        }
    }

