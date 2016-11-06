using HRPortal.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Models;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HRPortal.Data.Repositories
{
    public class FCategoryRepository : ICategoryRepository
    {
        public const string categoryPath = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\HRPortal\Categories.json";
        
        private List<Category> LoadCategories()
        {
            List<Category> result = new List<Category>();

            try
            {
                using (FileStream stream = new FileStream(categoryPath, FileMode.Open, FileAccess.Read))
                {
                    DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Category>));

                    object read = cereal.ReadObject(stream);

                    result = read as List<Category>;
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        private void SaveCategories(List<Category> categories)
        {
            using (FileStream stream = new FileStream(categoryPath, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer cereal = new DataContractJsonSerializer(typeof(List<Category>));

                cereal.WriteObject(stream, categories);
            }
        }

        public Category AddCategory(Category category)
        {
            List<Category> categories = LoadCategories();

            if (categories.Count > 0)
            {
                category.CategoryId = categories.Max(c => c.CategoryId) + 1;
            }
            else
            {
                category.CategoryId = 1;
            }

            categories.Add(category);

            SaveCategories(categories);

            return category;
        }

        public void DeleteCategory(int CategoryId)
        {
            List<Category> categories = LoadCategories();
            categories.RemoveAll(c => c.CategoryId == CategoryId);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return LoadCategories();
        }

        public Category GetCategory(int CategoryId)
        {
            List<Category> categories = LoadCategories();

            Category result = categories.FirstOrDefault(c => c.CategoryId == CategoryId);

            return result;
        }
    }
}
