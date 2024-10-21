namespace EvaluacionDesempenoApi.Util.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class CryptoHelper
    {
        private readonly IConfiguration _configuration;
        public CryptoHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Decrypt(string encryptedText)
        {
            var secretKey = _configuration.GetSection("encryptKey").Value;
            // Convertir el texto cifrado en bytes
            var fullCipher = Convert.FromBase64String(encryptedText);

            // La longitud del IV
            var ivSize = 16; // AES utiliza un IV de 16 bytes

            // Extraer el IV de los primeros 16 bytes
            var iv = new byte[ivSize];
            Array.Copy(fullCipher, 0, iv, 0, ivSize);

            // Extraer el texto cifrado
            var cipher = new byte[fullCipher.Length - ivSize];
            Array.Copy(fullCipher, ivSize, cipher, 0, cipher.Length);

            // Cifrar el texto
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream(cipher))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }

}
