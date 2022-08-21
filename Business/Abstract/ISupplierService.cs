using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISupplierService
    {
        IDataResult<List<Supplier>> GetAll();
        IResult Add(Supplier entity);
        IResult Update(Supplier entity);
        IResult Delete(Supplier entity);
        IDataResult<Supplier>? GetById(int id);
        IDataResult<Supplier>? GetByName(string name);
        IDataResult<Supplier> GetByPostCode(string postCode);
    }
}
