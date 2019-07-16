using System.Threading.Tasks;
using VehicleTracking.Data.Model;

namespace VehicleTracking.Service.Interface
{
    public interface IAccountService
    {
        Task<string> LogIn(LoginRequest model);
        Task<string> Register(RegisterRequest model);
    }
}