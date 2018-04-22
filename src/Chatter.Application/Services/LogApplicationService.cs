using AutoMapper;
using Chatter.Application.Interfaces;
using Chatter.Application.ViewModels;
using Chatter.Domain.Log;

namespace Chatter.Application.Services
{
    public class LogApplicationService : ILogApplicationService
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;

        public LogApplicationService(ILogRepository categoryRepository, IMapper mapper)
        {
            _logRepository = categoryRepository;
            _mapper = mapper;
        }

        public void Add(LogViewModel log)
        {
            _logRepository.Add(Mapper.Map<Log>(log));
            _logRepository.SaveChanges();
        }

        public void Dispose()
        {
            _logRepository.Dispose();
        }
    }
}