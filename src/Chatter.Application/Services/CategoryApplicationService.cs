using AutoMapper;
using Chatter.Application.Interfaces;
using Chatter.Application.ViewModels;
using Chatter.Domain.Categories.Commands;
using Chatter.Domain.Categories.Repository;
using Chatter.Domain.Core.Bus;
using System.Collections.Generic;

namespace Chatter.Application.Services
{
    public class CategoryApplicationService : ICategoryApplicationService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplicationService(IBus bus, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public void Add(CategoryViewModel category)
        {
            var categoryCommand = _mapper.Map<AddCategoryCommand>(category);
            _bus.SendCommand(categoryCommand);
        }

        public CategoryViewModel Get(int id)
        {
            return _mapper.Map<CategoryViewModel>(_categoryRepository.Get(id));
        }

        public IEnumerable<CategoryViewModel> GetAll(bool activeOnly = false)
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(_categoryRepository.GetAll(activeOnly));
        }

        public void Update(CategoryViewModel category)
        {
            var updateCommand = _mapper.Map<UpdateCategoryCommand>(category);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(int id)
        {
            _bus.SendCommand(new RemoveCategoryCommand(id));
        }

        public void Dispose()
        {
            _categoryRepository.Dispose();
        }
    }
}