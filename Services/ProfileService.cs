using AutoMapper;
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
        private readonly IGenericRepository<Profiles> _repositoryProfile;
        private readonly IUsersProfilesRepository _usersProfilesRepository;
        private readonly IMapper _mapper;
        public ProfileService(UserManager<ApplicationUser> userManager, 
            IGenericRepository<UsersProfiles> repository,
            IGenericRepository<Profiles> repositoryProfile,
            IUsersProfilesRepository usersProfilesRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _usersProfilesRepository = usersProfilesRepository;
            _mapper = mapper;
            _repositoryProfile = repositoryProfile;
        }
        public List<UserProfileDto> GetAllProfiles()
        {
            List<UserProfileDto> profiles = new List<UserProfileDto>();
            var entities = _repositoryProfile.GetAll();
            profiles = _mapper.Map<List<UserProfileDto>>(entities);
            return profiles;
        }

        public async Task<ResponseTransaction> AssignProfile(UserProfileDto profileDto)
        {
            ResponseTransaction response = new ResponseTransaction();

                try
                {
                    var user = await _userManager.FindByIdAsync(profileDto.idUser.ToString());
                if (user == null)
                {
                    response.error = "SI";
                    response.errorDetail = "Usuario no cuenta con accesos!";
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
        public async Task<ResponseTransaction> RemoveUserProfileAsync(UserProfileDto profileDto)
        {
            ResponseTransaction response = new ResponseTransaction();

            try
            {
                var user = await _userManager.FindByIdAsync(profileDto.idUser.ToString());
                if (user == null)
                {
                    response.error = "SI";
                    response.errorDetail = "Usuario no existe!";
                    return response;
                }

                var responseRemove= await _usersProfilesRepository.RemoveProfile(profileDto);
                if (responseRemove)
                {
                    response.error = "NO";
                    response.message = "Perfil removido exitosamentge!";
                }
                else
                {
                    response.error = "SI";
                    response.errorDetail = "Perfil no existe!";
                }
                
                return response;
            }
            catch (Exception ex)
            {

                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public async Task<List<UserProfileDto>> GetProfilesByUserID(int userId)
        {
            ResponseTransaction response = new ResponseTransaction();
            List<UserProfileDto> profiles = new List<UserProfileDto>();
            List<UsersProfiles> userProfiles = new List<UsersProfiles>();
            try
            {
                var entitiesProfiles = _repositoryProfile.GetAll();
                profiles = _mapper.Map<List<UserProfileDto>>(entitiesProfiles);

                if (userId != 0) {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user == null)
                    {
                        throw new Exception("Usuario no existe!");
                    }
                    else
                    {
                        userProfiles = await _usersProfilesRepository.GetProfilesEntityByUserID(userId);

                        if (userProfiles != null && userProfiles.Count > 0)
                        {
                            profiles.ForEach(p =>
                            {
                                if (userProfiles.Any(up => up.CdProfile == p.cdProfile))
                                {
                                    p.selected = true;
                                }
                            });
                        }
                    }
                } 
                
                
               
                
                return profiles;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
