using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetByName(string name);
        IResult Add(Color entity);
        IResult Update(Color entity);
        IResult Delete(Color entity);
    }
}
