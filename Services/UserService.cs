using Azure;
using Azure.Core;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEncryptionService _encryptionService;
        private readonly IGenericRepository<UsersProfiles> _repository;
        private readonly IUsersProfilesRepository _usersProfilesRepository;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<ApplicationUser> userManager,
            IEncryptionService encryptionService,
            IGenericRepository<UsersProfiles> repository,
            IUsersProfilesRepository usersProfilesRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _encryptionService = encryptionService;
            _repository = repository;
            _usersProfilesRepository = usersProfilesRepository;
            _configuration = configuration;
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

        public async Task<ResponseTransaction> Register(UserRegisterDto registerDto)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {

                // Verificar si el usuario ya existe
                var existingUser = await CheckIfUserExists(registerDto.username);
                if (existingUser != null)
                {
                    response.error = "SI";
                    response.errorDetail = "El Usuario ya existe!";
                    return response;
                }
                var decryptedPassword = _encryptionService.Decrypt(registerDto.password);
                registerDto.password = decryptedPassword;

                
                return await CreateUser(registerDto);

                
            }
            catch (Exception ex)
            {
                var detailedError = $"Error: {ex.Message}. StackTrace: {ex.StackTrace}";
                Debug.WriteLine(detailedError);  // Para tener más detalles en la consola de debug
                response.error = "SI";
                response.errorDetail = detailedError;
                return response;
            }


        }
        private async Task<ApplicationUser> CheckIfUserExists(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        private async Task<ResponseTransaction> CreateUser(UserRegisterDto registerDto)
        {
            ResponseTransaction response = new ResponseTransaction();

            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.username,
                    AltName = registerDto.altName,
                    AltEmail = registerDto.altEmail,
                    Email = registerDto.altEmail,
                    CdLanguage = registerDto.cdLanguage,
                    IdEmployee = registerDto.idEmployee,
                    IndChangePassword = registerDto.indChangePassword,
                    IndEnabled = true
                };

                var result = await _userManager.CreateAsync(user, registerDto.password);

                if (!result.Succeeded)
                {
                    response.error = "SI";
                    response.IsIdentityError = true;
                    response.errorsIdentity = result.Errors;
                }
                else
                {
                    response.error = "NO";
                    response.message = "Usuario registrado exitosamente!";
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

        public async Task<ResponseTransaction> UpdateUser(int id, UserDto userDto)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
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
                    response.message = "Usuario actualizado exitosamentge!";
                  
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

        public async Task<ResponseTransaction> Login(UserLoginDto loginDto)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                var user = await CheckIfUserExists(loginDto.username);

                if (user == null)
                {
                    response.error = "SI";
                    response.errorDetail = "Credenciales Inválidas!";
                    return response;
                }

                // Cargar explícitamente la entidad relacionada Employees
                //await _context.Entry(user).Reference(u => u.Employees).LoadAsync();
                

                var decryptedPassword = _encryptionService.Decrypt(loginDto.password);
                loginDto.password =  decryptedPassword;

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.password);
                if (!isPasswordValid)
                {
                    response.error = "SI";
                    response.errorDetail = "Credenciales Inválidas!";
                    return response;
                }



                user = await _usersProfilesRepository.GetEmployee(user);
                var token = await GenerateJwtToken(user);
                response.error = "NO";
                response.response = token;
                response.message = "Usuario verificado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {

                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }

        }
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            // Obtener los datos de configuración
            var jwtSettings = _configuration.GetSection("Jwt");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims (información adicional)
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("IndEnabled", user.IndEnabled.ToString()),  // Propiedad extendida
                new Claim("AltName", user.AltName ?? string.Empty),    // Propiedad extendida
                new Claim("CdLanguage", user.CdLanguage ?? string.Empty), // Propiedad extendida
                new Claim("IndChangePassword", user.IndChangePassword.ToString()) // Propiedad extendida
        
            };
            // Cargar los perfiles del usuario desde la tabla UsersProfiles
            var userProfiles = await _usersProfilesRepository.GetProfilesByUserID(user.Id);
            // Unir los nombres de los perfiles en un solo string separado por comas
            var profilesString = string.Join(",", userProfiles);

            // Agregar el string con los perfiles como un único claim
            userClaims.Add(new Claim("Profiles", profilesString));

            if(user.Employees != null)
            {
                userClaims.Add(new Claim("IdEmployee", user.IdEmployee.ToString()));
                userClaims.Add(new Claim("Names", String.Format("{0} {1}", user.Employees.Names, user.Employees.LastNames)));
                userClaims.Add(new Claim("Division", user.Employees.CdDivisions));
                userClaims.Add(new Claim("EmailCorp", user.Employees.Email));

            }


            // Crear el token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
