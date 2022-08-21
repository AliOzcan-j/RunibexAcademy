using Core.Utilities.Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        IDataResult<List<Brand>> GetAll();
        IResult Add(Brand entity);
        IResult Update(Brand entity);
        IResult Delete(Brand entity);
        IDataResult<Brand>? GetById(int id);
        IDataResult<Brand>? GetByName(string name);
    }
}
