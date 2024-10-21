namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Decrypt(string encryptedPassword);
    }
}
