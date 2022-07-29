using Business.Abstract;
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
            _fuelTypeDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(FuelType entity)
        {
            _fuelTypeDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<FuelType>> GetAll()
        {
            return new SuccessDataResult<List<FuelType>>(_fuelTypeDal.GetAllWithoutTracker());
        }

        public IDataResult<FuelType> GetByName(string name)
        {
            return new SuccessDataResult<FuelType>(_fuelTypeDal.Get(p => p.Name == name));
        }

        public IResult Update(FuelType entity)
        {
            _fuelTypeDal.Update(entity);
            return new SuccessResult();
        }
    }
}
