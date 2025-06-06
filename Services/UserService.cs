using AutoMapper;
using Azure;
using Azure.Core;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;
using EvaluacionDesempenoApi.Util.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EvaluacionDesempenoApi.Services
{
    public class UserService : IUserService
    {
        
        private readonly IEncryptionService _encryptionService;
        private readonly IGenericRepository<UsersProfiles> _repository;
        private readonly IUsersProfilesRepository _usersProfilesRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager,
            IEncryptionService encryptionService,
            IGenericRepository<UsersProfiles> repository,
            IUsersProfilesRepository usersProfilesRepository,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _encryptionService = encryptionService;
            _repository = repository;
            _usersProfilesRepository = usersProfilesRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                var userDto = new UserDto
                {
                    username = user.UserName,
                    altName = user.AltName,
                    altEmail = user.AltEmail,
                    cdLanguage = user.CdLanguage,
                    indEnabled = user.IndEnabled
                };
                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }

        }
       
        

        public async Task<ResponseTransaction> UpdateUser(UserDto userDto)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userDto.id);
                if (user == null) {
                    throw new Exception("Usuario no encontrado");
                }

                // Update extended fields
                user.AltName = userDto.altName;
                user.AltEmail = userDto.altEmail;
                user.CdLanguage = userDto.cdLanguage;
                user.IndEnabled = userDto.indEnabled;
                user.IndChangePassword = userDto.indChangePassword;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    response.error = "SI";
                    response.IsIdentityError = true;
                    response.errorsIdentity = result.Errors;
                }
                else {
                    response.error = "NO";
                    response.message = "Usuario actualizado exitosamente!";
                  
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

        public async Task<ResponseTransaction> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {

                var user = await _userManager.FindByNameAsync(changePasswordDto.username);
                if (user == null)
                {
                    response.error = "SI";
                    response.errorDetail = "El Usuario no existe!";
                    return response;
                }

                var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);
                if (!result.Succeeded)
                {
                    response.error = "SI";
                    response.IsIdentityError = true;
                    response.errorsIdentity = result.Errors;
                }



                response.error = "NO";
                response.message = "Contraseña cambiado exitosamentge!";
                return response;
            }
            catch (Exception ex)
            {

                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        

        public async Task<UserDto> GetUserByEmployeeId(int idEmployee)
        {
            try
            {
                UserDto userDto = null;
                // Obtener el usuario relacionado con el IdEmployee
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.IdEmployee == idEmployee);

                if (user != null)
                {
                    userDto = new UserDto();
                    userDto.id = user.Id;
                    userDto.idEmployee = user.IdEmployee;
                    userDto.username = user.UserName;
                    userDto.altName = user.AltName;
                    userDto.altEmail = user.AltEmail;
                    userDto.cdLanguage = user.CdLanguage;
                    userDto.indEnabled = user.IndEnabled;
                };

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

    }
}
