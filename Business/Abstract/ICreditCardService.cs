using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCardForStoreDto creditCardForStoreDto);
        IResult Delete(int id);
        IDataResult<CreditCard> Get(int id);
    }
}
