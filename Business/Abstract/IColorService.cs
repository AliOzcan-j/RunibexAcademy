using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll(Expression<Func<Color, bool>> filter = null);
        IResult Add(Color entity);
        IResult Update(Color entity);
        IResult Delete(Color entity);
        IDataResult<Color>? GetById(int id);
        IDataResult<Color>? GetByName(string name);
    }
}
