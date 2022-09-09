using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICurrencyService
    {
        IDataResult<List<Currency>> GetAll(Expression<Func<Currency, bool>> filter = null);
        IResult Add(Currency entity);
        IResult Update(Currency entity);
        IResult Delete(Currency entity);
        IDataResult<Currency>? GetById(int id);
        IDataResult<Currency>? GetByName(string name);
        IDataResult<Currency> GetByIsoCode(string isoCode);
    }
}
