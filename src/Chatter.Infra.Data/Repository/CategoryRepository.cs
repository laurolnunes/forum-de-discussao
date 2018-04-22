using System.Collections.Generic;
using Chatter.Domain.Categories;
using Chatter.Domain.Categories.Repository;
using Chatter.Infra.Data.Context;
using Chatter.Infra.Data.Repository.Base;

namespace Chatter.Infra.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ChatterContext db) : base(db)
        {
            
        }

        public override IEnumerable<Category> GetAll(bool activeOnly = false)
        {
            return activeOnly ? Find(c => c.Active) : base.GetAll();
        }

        public override void Remove(int id)
        {
            var category = Get(id);
            category.SetRemoved();
            Update(category);
        }
    }
}