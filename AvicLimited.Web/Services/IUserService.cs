namespace AvicLimited.Web.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}