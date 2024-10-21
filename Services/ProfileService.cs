using Azure;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Services
{
    public class ProfileService: IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<UsersProfiles> _repository;
        public ProfileService(UserManager<ApplicationUser> userManager, IGenericRepository<UsersProfiles> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<ResponseTransaction> AssignProfile(UserProfileDto profileDto)
        {
            ResponseTransaction response = new ResponseTransaction();

                try
                {
                    var user = await _userManager.FindByIdAsync(profileDto.userId);
                if (user == null)
                {
                    response.error = "SI";
                    response.errorDetail = "Usuario no existe!";
                    return response;
                }

                var userProfile = new UsersProfiles
                {
                    IdUser = user.Id,
                    CdProfile = profileDto.cdProfile
                };

                await _repository.AddAsync(userProfile);

                response.error = "NO";
                response.message = "Perfil asignado exitosamentge!";
                return response;
            }
            catch (Exception ex)
            {

                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }
    }
}
