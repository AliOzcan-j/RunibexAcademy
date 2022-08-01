using Business.Abstract;
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

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Car entity)
        {
            return _carDal.Delete(entity) == true 
                ? new SuccessResult() 
                : new ErrorResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker());
        }

        public IDataResult<List<Car>> GetBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAllWithoutTracker(x => x.BrandId == id));
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
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

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }
    }
}
