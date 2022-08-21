using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IResult Add(Color entity);
        IResult Update(Color entity);
        IResult Delete(Color entity);
        IDataResult<Color>? GetById(int id);
        IDataResult<Color>? GetByName(string name);
    }
}
