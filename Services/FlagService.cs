using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class FlagService : IFlagService
    {
        private readonly IGenericRepository<Flags> _flagsGenericRepository;
        private readonly IGenericRepository<FlagRules> _flagsRulesGenericRepository;
        private readonly IFlagRepository _flagRepository;
        private readonly IMapper _mapper;

        public FlagService(IGenericRepository<Flags> flagsGenericRepository,
            IGenericRepository<FlagRules> flagsRulesGenericRepository,
            IFlagRepository flagRepository,
            IMapper mapper)
        {
            _flagsGenericRepository = flagsGenericRepository;
            _flagsRulesGenericRepository = flagsRulesGenericRepository;
            _flagRepository = flagRepository;
            _mapper = mapper;
        }
        public FlagsDto GetFlagById(int idFlag)
        {
            try {
                FlagsDto flag = new FlagsDto();
                var entity = _flagRepository.GetFlag(idFlag);
                if (entity != null)
                {
                    flag = _mapper.Map<FlagsDto>(entity);

                }
                return flag;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<FlagsDto> GetFlags()
        {
            try
            {
                var entities = _flagsGenericRepository.GetAll();
                return _mapper.Map<List<FlagsDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public ResponseTransaction SaveFlag(FlagsDto flagData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (flagData == null || flagData.flagRules == null)
            {
                response.error = "SI";
                response.errorDetail = "Faltan datos de la bandera";
                return response;
            }
            try
            {
                var flag = _mapper.Map<Flags>(flagData);
                flag.CreateDate = DateTime.Now.ToUniversalTime();

                _flagsGenericRepository.Add(flag);

                response.error = "NO";
                response.message = "El flag fue creado exitosamente!";
                return response;

            }
            catch (Exception ex)
            {

                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction UpdateFlag(FlagsDto flagData)
        {
            ResponseTransaction response = new ResponseTransaction();
            if (flagData == null || flagData.flagRules == null)
            {
                response.error = "SI";
                response.errorDetail = "Faltan datos de la bandera";
                return response;
            }
            try
            {
                _flagRepository.UpdateFlagRules(flagData);

                response.error = "NO";
                response.message = "El flag fue actualizado exitosamente!";
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
