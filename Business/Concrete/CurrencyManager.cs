using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        ICurrencyDal _currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            _currencyDal = currencyDal;
        }

        public IResult Add(Currency entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result != null)
            {
                return result;
            }
            _currencyDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICurrencyService.Get")]
        public IResult Delete(Currency entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _currencyDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<Currency>>))]
        public IDataResult<List<Currency>> GetAll(Expression<Func<Currency, bool>> filter = null)
        {
            return new SuccessDataResult<List<Currency>>(_currencyDal.GetAllWithoutTracker(filter));
        }

        [CacheAspect(typeof(DataResult<Currency>))]
        public IDataResult<Currency>? GetById(int id)
        {
            var result = _currencyDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Currency>(result);
            }
            return new ErrorDataResult<Currency>();
        }

        [CacheAspect(typeof(DataResult<Currency>))]
        public IDataResult<Currency> GetByIsoCode(string isoCode)
        {
            var result = _currencyDal.Get(c => c.IsoCode.Equals(isoCode));
            if (result != null)
            {
                return new SuccessDataResult<Currency>(result);
            }
            return new ErrorDataResult<Currency>();
        }

        [CacheAspect(typeof(DataResult<Currency>))]
        public IDataResult<Currency>? GetByName(string name)
        {
            var result = _currencyDal.Get(c => c.Name.Equals(name));
            if (result != null)
            {
                return new SuccessDataResult<Currency>(result);
            }
            return new ErrorDataResult<Currency>();
        }

        [CacheRemoveAspect("ICurrencyService.Get")]
        public IResult Update(Currency entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _currencyDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(string name)
        {
            var result = GetByName(name);
            if (result.Success)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
