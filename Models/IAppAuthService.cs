namespace JWT_Auth_WebApi.Models
{
    public interface IAppAuthService
    {
        Task<Token> Authentification(LoginUser loginUser);
    }
}
