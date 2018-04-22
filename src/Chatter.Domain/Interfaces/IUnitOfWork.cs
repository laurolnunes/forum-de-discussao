using System;
using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}