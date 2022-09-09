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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if(result != null)
            {
                return result;
            }

            _brandDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }

            _brandDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<Brand>))]
        public IDataResult<Brand> GetByName(string name)
        {
            var result = _brandDal.Get(b => b.Name == name);
            if (result != null)
            {
                return new SuccessDataResult<Brand>(result);
            }
            return new ErrorDataResult<Brand>();
        }

        [CacheAspect(typeof(IDataResult<List<Brand>>))]
        public IDataResult<List<Brand>> GetAll(Expression<Func<Brand, bool>> filter)
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAllWithoutTracker(filter));
        }
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _brandDal.Update(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(IDataResult<Brand>))]
        public IDataResult<Brand> GetById(int id)
        {
            var result = _brandDal.Get(b => b.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Brand>(result);
            }
            return new ErrorDataResult<Brand>();
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
