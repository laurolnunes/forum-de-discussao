using System;
using System.Linq;
using AutoMapper;
using Chatter.Application.Interfaces;
using Chatter.Application.ViewModels;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Users.Commands;
using Chatter.Domain.Users.Repository;

namespace Chatter.Application.Services
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserApplicationService(IBus bus, IMapper mapper, IUserRepository userRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public void Add(UserViewModel userViewModel)
        {
            var user = _mapper.Map<AddUserCommand>(userViewModel);
            _bus.SendCommand(user);
        }

        public int GetIdByIdentityId(Guid id)
        {
            return _userRepository.GetIdByIdentityId(id);
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }
    }
}