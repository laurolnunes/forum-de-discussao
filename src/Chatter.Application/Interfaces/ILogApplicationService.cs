using System;
using Chatter.Application.ViewModels;

namespace Chatter.Application.Interfaces
{
    public interface ILogApplicationService : IDisposable
    {
        void Add(LogViewModel category);
    }
}