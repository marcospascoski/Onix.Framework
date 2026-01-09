using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onix.Framework.Security.JwtConfig
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, int keySizeInBytes = 16)
        {
            // Configurar Autenticação JWT com suas configurações específicas
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Definir como true para produção
                        ValidateAudience = true, // Definir como true para produção
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = GetSigningKey(keySizeInBytes), // Usar o método para obter a chave secreta
                        ClockSkew = TimeSpan.Zero // Definir um desvio de relógio apropriado para validação do token
                    };
                });

            return services;
        }

        private static SymmetricSecurityKey GetSigningKey(int keySizeInBytes)
        {
            // Utiliza a classe EncryptionHelper para obter uma chave segura
            var secret = EncryptionHelper.GenerateRandomSecret(keySizeInBytes);
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        public static string GerarAccessToken(List<Claim> claims, int keySizeInBytes = 128)
        {
            var signingCredentials = new SigningCredentials(
                GetSigningKey(keySizeInBytes),
                SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), // Defina um tempo de expiração curto para tokens de acesso
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
        public static Task InvalidarTokenAsync(string token)
        {
            // Implemente a lógica para invalidar o token aqui
            return Task.CompletedTask;
        }
    }
}
