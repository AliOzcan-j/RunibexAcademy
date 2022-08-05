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

        public IResult Delete(Supplier entity)
        {
            _supplierDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Supplier>> GetAll()
        {
            return new SuccessDataResult<List<Supplier>>(_supplierDal.GetAllWithoutTracker());
        }

        public IDataResult<Supplier>? GetById(int id)
        {
            return new SuccessDataResult<Supplier>(_supplierDal.Get(s => s.Id == id));
        }

        public IDataResult<Supplier>? GetByName(string name)
        {
            return new SuccessDataResult<Supplier>(_supplierDal.Get(s => s.CompanyName.Equals(name)));
        }

        public IDataResult<Supplier> GetByPostCode(string postCode)
        {
            return new SuccessDataResult<Supplier>(_supplierDal.Get(s => s.Postcode.Equals(postCode)));
        }

        public IResult Update(Supplier entity)
        {
            _supplierDal.Update(entity);
            return new SuccessResult();
        }
    }
}
