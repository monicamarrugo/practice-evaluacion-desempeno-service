using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{  
    public class FlagRepository : IFlagRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public FlagRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Flags GetFlag(int idFlag)
        {
            return _dbContext.Flags
               .Include(e => e.FlagRules)
               .Where(r => r.IdFlag == idFlag)
               .FirstOrDefault();
        }

        public void UpdateFlagRules(FlagsDto flagData)
        {
            // Recuperar reglas existentes
            var flag = this.GetFlag(flagData.idFlag);
            flag.NameFlagES = flagData.nameFlagES;
            flag.NameFlagEN = flagData.nameFlagEN;
            flag.ModifiedDate =   flagData.modifiedDate;
            var currentRules = flag.FlagRules;

            // Actualizar las reglas existentes y agregar las nuevas
            foreach (var uRules in flagData.flagRules)
            {
                var existingRule = currentRules.FirstOrDefault(q => q.IdFlagRules == uRules.idFlagRules);
                if (existingRule != null)
                {
                    _mapper.Map(uRules, existingRule);
                }
                else
                {
                    var newRule = _mapper.Map<FlagRules>(uRules);
                    _dbContext.FlagRules.Add(newRule);
                }
            }

            // Eliminar las preguntas existentes que no están en la lista nueva
            foreach (var existingRule in currentRules)
            {
                if (!flagData.flagRules.Any(q => q.idFlagRules == existingRule.IdFlagRules))
                {
                    _dbContext.FlagRules.Remove(existingRule);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
