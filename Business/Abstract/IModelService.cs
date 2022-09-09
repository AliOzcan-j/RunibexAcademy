using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IModelService
    {
        IDataResult<List<Model>> GetAll(Expression<Func<Model, bool>> filter = null);
        IResult Add(Model entity);
        IResult Update(Model entity);
        IResult Delete(Model entity);
        IDataResult<Model>? GetById(int id);
        IDataResult<Model>? GetByName(string name);
        IDataResult<Model> GetByBrandId(int id);
    }
}
