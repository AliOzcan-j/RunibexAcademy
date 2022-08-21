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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FuelTypeManager : IFuelTypeService
    {
        IFuelTypeDal _fuelTypeDal;

        public FuelTypeManager(IFuelTypeDal fuelTypeDal)
        {
            _fuelTypeDal = fuelTypeDal;
        }

        public IResult Add(FuelType entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result != null)
            {
                return result;
            }
            _fuelTypeDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IFuelTypeService.Get")]
        public IResult Delete(FuelType entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _fuelTypeDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<FuelType>>))]
        public IDataResult<List<FuelType>> GetAll()
        {
            return new SuccessDataResult<List<FuelType>>(_fuelTypeDal.GetAllWithoutTracker());
        }

        [CacheAspect(typeof(DataResult<FuelType>))]
        public IDataResult<FuelType> GetByName(string name)
        {
            var result = _fuelTypeDal.Get(ft => ft.Name == name);
            if (result != null)
            {
                return new SuccessDataResult<FuelType>(result);
            }
            return new ErrorDataResult<FuelType>();
        }

        [CacheAspect(typeof(DataResult<FuelType>))]
        public IDataResult<FuelType> GetById(int id)
        {
            var result = _fuelTypeDal.Get(ft => ft.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<FuelType>(result);
            }
            return new ErrorDataResult<FuelType>();
        }

        [CacheRemoveAspect("IFuelTypeService.Get")]
        public IResult Update(FuelType entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _fuelTypeDal.Update(entity);
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
