﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IModelService
    {
        IDataResult<List<Model>> GetAll();
        IDataResult<Model> GetByName(string name);
        IDataResult<Model> GetByBrandId(int id);
        IResult Add(Model entity);
        IResult Update(Model entity);
        IResult Delete(Model entity);
    }
}
