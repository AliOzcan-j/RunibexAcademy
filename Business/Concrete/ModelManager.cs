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
using System.Linq.Expressions;
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

        [CacheRemoveAspect("IModelService.Get")]
        public IResult Delete(Model entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.NameSuffix));

            if (result == null)
            {
                return result;
            }
            _modelDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<Model>>))]
        public IDataResult<List<Model>> GetAll(Expression<Func<Model, bool>> filter = null)
        {
            return new SuccessDataResult<List<Model>>(_modelDal.GetAllWithoutTracker(filter));
        }

        [CacheAspect(typeof(DataResult<Model>))]
        public IDataResult<Model> GetByBrandId(int id)
        {
            var result = _modelDal.Get(m => m.BrandId == id);
            if (result != null)
            {
                return new SuccessDataResult<Model>(result);
            }
            return new ErrorDataResult<Model>();
        }

        [CacheAspect(typeof(DataResult<Model>))]
        public IDataResult<Model> GetById(int id)
        {
            var result =_modelDal.Get(m => m.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Model>(result);
            }
            return new ErrorDataResult<Model>();
        }

        [CacheAspect(typeof(DataResult<Model>))]
        public IDataResult<Model> GetByName(string name)
        {
            var result = _modelDal.Get(m => $"{m.NamePrefix} {m.NameSuffix}" == name);
            if (result != null)
            {
                return new SuccessDataResult<Model>(result);
            }
            return new ErrorDataResult<Model>();
        }

        [CacheRemoveAspect("IModelService.Get")]
        public IResult Update(Model entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.NameSuffix));

            if (result == null)
            {
                return result;
            }
            _modelDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfExists(string name)
        {
            var result = GetByName(name);
            if (result.Success)
            {
                return new ErrorResult(Messages.ThisRecordExists);
            }
            return new SuccessResult();
        }
    }
}
