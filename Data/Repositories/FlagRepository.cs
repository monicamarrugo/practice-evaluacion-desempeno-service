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

        public void UpdateFlag(CreateFlagDto flagData)
        {
            throw new NotImplementedException();
        }
    }
}
