using HRPortal.Data.Interfaces;
using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.Repositories
{
    public class MCategoryRepository : ICategoryRepository
    {
        private static List<Category> _categories;

        static MCategoryRepository()
        {
            // sample data
            _categories = new List<Category>
                {
                    new Category { CategoryId=1, CategoryName="General"},
                    new Category { CategoryId=2, CategoryName="Benefits" },
                    new Category { CategoryId=3, CategoryName="Workplace Health and Safety" }
                };
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategory(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public Category AddCategory(Category category)
        {
            category.CategoryId = _categories.Max(c => c.CategoryId) + 1;

            _categories.Add(category);

            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            _categories.RemoveAll(c => c.CategoryId == categoryId);
        }

    }
}