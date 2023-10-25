namespace JWT_Auth_WebApi.Models
{
    public class AppAuthrizService : IAppAuthrizService
    {
        private readonly IConfiguration _configuration;

        Dictionary<string, string> TestUsers = new Dictionary<string, string>
        {
            {"user", "user"},
        };

        public AppAuthrizService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Token> Authorization(User user)
        {
            throw new NotImplementedException();
        }
    }
}
