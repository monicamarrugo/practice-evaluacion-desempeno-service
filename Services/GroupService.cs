using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class GroupService: IGroupService
    {
        private readonly IGenericRepository<Groups> _repository;

        public GroupService(IGenericRepository<Groups> repository)
        {
            _repository = repository;
        }
        public List<Groups> GetAllGroups()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }
    }
}
