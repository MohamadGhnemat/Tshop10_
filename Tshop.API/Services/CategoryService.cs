using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tshop.API.Data;
using Tshop.API.Models;

namespace Tshop.API.Services
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = _context.Categories.AsNoTracking().FirstOrDefault(c=>c.Id == id);
            if (categoryInDb == null) return false;
            category.Id =id;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public bool Remove(int id)
        {
            Category? categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null) return false;
            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return true;
        }
    

        

       
    }
}
