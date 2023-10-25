namespace JWT_Auth_WebApi.Models
{
    public interface IAppAuthrizService
    {
        Task<Token> Authorization(User user);
    }
}
