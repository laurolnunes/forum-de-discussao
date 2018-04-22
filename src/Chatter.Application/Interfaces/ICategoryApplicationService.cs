using System;
using System.Collections.Generic;
using Chatter.Application.ViewModels;

namespace Chatter.Application.Interfaces
{
    public interface ICategoryApplicationService : IDisposable
    {
        void Add(CategoryViewModel category);

        void Remove(int id);

        CategoryViewModel Get(int id);

        IEnumerable<CategoryViewModel> GetAll(bool activeOnly = false);

        void Update(CategoryViewModel category);
    }
}