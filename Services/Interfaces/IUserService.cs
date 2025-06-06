using EvaluacionDesempenoApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IUserService
    {
        
        Task<UserDto> GetUserById(int id);

        Task<ResponseTransaction> UpdateUser(UserDto userDto);

        Task<ResponseTransaction> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<UserDto> GetUserByEmployeeId(int idEmployee);
    }
}
