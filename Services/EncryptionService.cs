using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

namespace EvaluacionDesempenoApi.Services
{
    public class EncryptionService: IEncryptionService
    {
        private readonly IConfiguration _configuration;

        public EncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Decrypt(string encryptedPassword)
        {
            try
            {
                var key = _configuration.GetSection("encryptKey").Value;
                var iv = _configuration.GetSection("encryptIv").Value;
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedPassword);
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;

                    using (MemoryStream ms = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cs, Encoding.UTF8))
                            {
                                return reader.ReadToEnd(); // Retorna la contraseña desencriptada
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desencriptar la contraseña", ex);
            }
        }
    }
}
