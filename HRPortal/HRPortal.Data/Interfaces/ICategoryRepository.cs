using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();

        Category GetCategory(int CategoryId);

        Category AddCategory(Category category);

        void DeleteCategory(int CategoryId);
    }
}
