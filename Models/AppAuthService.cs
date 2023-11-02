using Azure.Identity;
using JWT_Auth_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWT_Auth_WebApi.Models
{
    public class AppAuthService : IAppAuthService
    {
        private readonly IConfiguration _configuration;

        //Creating DbUser list acting like a real database 
        List<User> DbUsers = new List<User>
        {
            new User { Username = "admin", Password = "pwd", Role = "admin"},
            new User { Username = "user", Password = "pwd", Role = "user"},
            new User { Username = "guest", Password = "pwd", Role = "guest"}
        };

        public AppAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Token> Authentification(LoginUser loginUser)
        {
            //We check the inputs if its correct
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
                throw new Exception($"Invalid input received!");

            //Checking if the informations of the user are correct in the database
            var user = DbUsers.Find(User => User.Username.Equals(loginUser.Username) && User.Password.Equals(loginUser.Password));

            if (user == null)
                throw new Exception($"Username with the name {loginUser.Username} is not found or Password is Invalide!");

            //Generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature), 
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token { AuthToken = tokenHandler.WriteToken(token)};
        }
    }
}
