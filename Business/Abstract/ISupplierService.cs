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
    public interface ISupplierService : IEntityServiceBase<Supplier>, IEntityServiceAddon<Supplier>
    {
        IDataResult<Supplier> GetByPostCode(string postCode);
    }
}
