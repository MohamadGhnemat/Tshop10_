using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Tshop.API.Models;

namespace Tshop.API.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
      Category?  Get(Expression <Func<Category,bool>> expression);
        Category Add(Category category);
        bool Edit(int id, Category category);
        bool Remove(int id);
        


    }
}
