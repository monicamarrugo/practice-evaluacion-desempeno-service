using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEscaleService
    {
        public List<Escales> GetAllEscales();
        public List<EscalesValues> GetAllEscalesValues();
    }
}
