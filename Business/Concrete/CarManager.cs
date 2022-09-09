using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICacheManager _cacheManager;

        public CarManager(ICarDal carDal, ICacheManager cacheManager)
        {
            _carDal = carDal;
            _cacheManager = cacheManager;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car entity)
        {
            return _carDal.Delete(entity) == true 
                ? new SuccessResult() 
                : new ErrorResult();
        }
        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(filter));
        }

        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetBySupplierId(int id)
        {
            var result = _carDal.GetAllWithoutTracker(x => x.SupplierId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>();
            }
            return new ErrorDataResult<List<Car>>();
        }

        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetByBrandId(int id)
        {
            var result = _carDal.GetAllWithoutTracker(x => x.Model.BrandId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            return new ErrorDataResult<List<Car>>();
        }

        [CacheAspect(typeof(DataResult<Car>))]
        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Car>(result);
            }
            return new ErrorDataResult<Car>("doesnt exists");
        }

        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetByColorId(int id)
        {
            var result = _carDal.GetAllWithoutTracker(x => x.ColorId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            return new ErrorDataResult<List<Car>>();
        }

        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetByFuleTypeId(int id)
        {
            var result = _carDal.GetAllWithoutTracker(x => x.FuelTypeId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            return new ErrorDataResult<List<Car>>();
        }

        [CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetByModelId(int id)
        {
            var result = _carDal.GetAllWithoutTracker(x => x.ModelId == id);
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            return new ErrorDataResult<List<Car>>();
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<CarDetailDto>>))]
        public IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            var result = _carDal.GetCarDetails(filter);
            if (result.Any())
            {
                return new SuccessDataResult<List<CarDetailDto>>(result);
            }
            return new ErrorDataResult<List<CarDetailDto>>();
        }
    }
}
