using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetBrandId(int id);
        IDataResult<Car> GetModelId(int id);
        IDataResult<List<Car>> GetColorId(int id);
        IDataResult<List<Car>> GetFuleTypeId(int id);
        IResult Add(Car entity);
        IResult Update(Car entity);
        IResult Delete(Car entity);
    }
}
