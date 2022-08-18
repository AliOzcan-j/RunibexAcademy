using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        [CacheRemoveAspect("IEntityServiceBase.Get")]
        public IResult Delete(Car entity)
        {
            return _carDal.Delete(entity) == true 
                ? new SuccessResult() 
                : new ErrorResult();
        }

        //[CacheAspect(typeof(DataResult<List<Car>>))]
        public IDataResult<List<Car>> GetAll()
        {
            var result = _carDal.GetAll();
            if (result.Any())
            {
                return new SuccessDataResult<List<Car>>(result);
            }
            return new ErrorDataResult<List<Car>>("no record exists");
        }

        public IDataResult<List<Car>> GetSupplierId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker(x => x.SupplierId == id));
        }
        public IDataResult<List<Car>> GetBrandId(int id)
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

        public IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(filter));
        }

        public IDataResult<List<Car>> GetColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker(x => x.ColorId == id));
        }

        public IDataResult<List<Car>> GetFuleTypeId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker( x => x.FuelTypeId == id));
        }

        public IDataResult<List<Car>> GetModelId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker( x => x.ModelId == id));
        }

        [CacheRemoveAspect("IEntityServiceBase.Get")]
        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }
    }
}
