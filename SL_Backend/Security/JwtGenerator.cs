namespace SL_Backend.Security
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class JwtTokenGenerator
    {
        private const string SecretKey = "d72ada5666d21f44848b4c342bf79e6cea9b41e19a3673988e90526eff256eb916c0e2e1ea612e9bfdec86bdd40abd30106e9ab7e9c65dab18f7445237f7d53733dc91a889d73bb0c07ae713c80df3e0213159411f20dcc5b983b981651e3c1b88c4ab70e339040b34f5e86a76b91c948be28737d4ebff0cc5444fbe591049ff43a876356268b44e18216b81a132f3028de2802cdc3ffd562bf741761a4b0c34db0d4faad63bbb17b23ec19fc4483d4e6c3a7bdc23912ea13ab0d201a69eb1b9c425ec297ce8d0d419897d5710e345bbb7a3cd6495e4e6a4c9a1e3e51aef8ae1d089e5bf0346693dcb1676240fb91eb4a8a2627a322f48c96e75a4d44d7f3979";
        private const string Issuer = "SL_Backend";
        private const string Audience = "Users";

        public static string GenerateToken(string username, int expireMinutes = 60)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
