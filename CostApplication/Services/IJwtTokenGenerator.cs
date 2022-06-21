using CostApplication.Models;


namespace CostApplication.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
