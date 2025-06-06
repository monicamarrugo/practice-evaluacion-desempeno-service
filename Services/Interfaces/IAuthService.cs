using EvaluacionDesempenoApi.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseTransaction> Login(UserLoginDto user);
        Task<ResponseTransaction> Register(UserRegisterDto user);
    }
}
