using System.Threading.Tasks;
using VT.Data.Model;

namespace VT.Service.Interface
{
    public interface IAccountService
    {
        Task<string> LogIn(LoginRequest model);
        Task<string> Register(RegisterRequest model);
    }
}