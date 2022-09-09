using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CountryManager : ICountryService
    {
        ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public IResult Add(Country entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result != null)
            {
                return result;
            }

            _countryDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICountryService.Get")]
        public IResult Delete(Country entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }

            _countryDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<Country>>))]
        public IDataResult<List<Country>> GetAll(Expression<Func<Country, bool>> filter = null)
        {
            return new SuccessDataResult<List<Country>>(_countryDal.GetAllWithoutTracker(filter));
        }

        [CacheAspect(typeof(DataResult<Country>))]
        public IDataResult<Country>? GetById(int id)
        {
            var result = _countryDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Country>(result);
            }
            return new ErrorDataResult<Country>();
        }
        [CacheAspect(typeof(DataResult<Country>))]
        public IDataResult<Country> GetByIsoCode(string isoCode)
        {
            var result = _countryDal.Get(c => c.IsoCode.Equals(isoCode));
            if (result != null)
            {
                return new SuccessDataResult<Country>(result);
            }
            return new ErrorDataResult<Country>();
        }

        [CacheAspect(typeof(DataResult<Country>))]
        public IDataResult<Country>? GetByName(string name)
        {
            var result = _countryDal.Get(c => c.Name.Equals(name));
            if (result != null)
            {
                return new SuccessDataResult<Country>(result);
            }
            return new ErrorDataResult<Country>();
        }
        [CacheRemoveAspect("ICountryService.Get")]
        public IResult Update(Country entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _countryDal.Update(entity);
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
