using CostApplication.Models;


namespace CostApplication.Services
{
    public interface IJwtTokenBuilder
    {
        string BuildToken(User user);
    }
}
