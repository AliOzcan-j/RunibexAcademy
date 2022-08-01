using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IModelService : IEntityServiceBase<Model>
    {
        IDataResult<Model> GetByBrandId(int id);
    }
}
