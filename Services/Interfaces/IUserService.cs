using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseTransaction> Register(UserRegisterDto registerDto);

        Task<UserDto> GetUserById(int id);

        Task<ResponseTransaction> UpdateUser(int id, UserDto userDto);

        Task<ResponseTransaction> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<ResponseTransaction> Login(UserLoginDto loginDto);
        Task<UserDto> GetUserByEmployeeId(int idEmployee);
    }
}
