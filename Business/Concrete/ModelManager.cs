using Business.Abstract;
using Business.Constants;
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
    public class ModelManager : IModelService
    {
        IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        public IResult Add(Model entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.NameSuffix));

            if (result != null)
            {
                return result;
            }

            _modelDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Model entity)
        {
            _modelDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Model>> GetAll()
        {
            return new SuccessDataResult<List<Model>>(_modelDal.GetAllWithoutTracker());
        }
        public IDataResult<Model> GetByBrandId(int id)
        {
            return new SuccessDataResult<Model>(_modelDal.Get(m => m.BrandId == id));
        }

        public IDataResult<Model> GetById(int id)
        {
            return new SuccessDataResult<Model>(_modelDal.Get(m => m.Id == id));
        }

        public IDataResult<Model> GetByName(string name)
        {
            return new SuccessDataResult<Model>(_modelDal.Get(m => $"{m.NamePrefix} {m.NameSuffix}" == name));
        }

        public IResult Update(Model entity)
        {
            _modelDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(string name)
        {
            var result = GetByName(name).Data;
            if (result != null)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
