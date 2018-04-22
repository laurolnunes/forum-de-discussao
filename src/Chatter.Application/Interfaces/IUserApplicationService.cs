using System;
using Chatter.Application.ViewModels;

namespace Chatter.Application.Interfaces
{
    public interface IUserApplicationService : IDisposable
    {
        void Add(UserViewModel userViewModel);

        int GetIdByIdentityId(Guid id);
    }
}