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
    public class SupplierManager : ISupplierService
    {
        ISupplierDal _supplierDal;

        public SupplierManager(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
        }

        public IResult Add(Supplier entity)
        {
            _supplierDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ISupplierService.Get")]
        public IResult Delete(Supplier entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.CompanyName));
            if (result == null)
            {
                return result;
            }
            _supplierDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Supplier>> GetAll()
        {
            return new SuccessDataResult<List<Supplier>>(_supplierDal.GetAllWithoutTracker());
        }

        [CacheAspect(typeof(DataResult<Supplier>))]
        public IDataResult<Supplier>? GetById(int id)
        {
            var result = _supplierDal.Get(s => s.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Supplier>(result);
            }
            return new ErrorDataResult<Supplier>();
        }

        [CacheAspect(typeof(DataResult<Supplier>))]
        public IDataResult<Supplier>? GetByName(string name)
        {
            var result = _supplierDal.Get(s => s.CompanyName.Equals(name));
            if (result != null)
            {
                return new SuccessDataResult<Supplier>(result);
            }
            return new ErrorDataResult<Supplier>();
        }

        [CacheAspect(typeof(DataResult<Supplier>))]
        public IDataResult<Supplier> GetByPostCode(string postCode)
        {
            var result = _supplierDal.Get(s => s.Postcode.Equals(postCode));
            if (result != null)
            {
                return new SuccessDataResult<Supplier>(result);
            }
            return new ErrorDataResult<Supplier>();
        }

        [CacheRemoveAspect("ISupplierService.Get")]
        public IResult Update(Supplier entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.CompanyName));
            if (result == null)
            {
                return result;
            }
            _supplierDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(string name)
        {
            var result = _supplierDal.Get(x => x.CompanyName == name);
            if (result != null)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
