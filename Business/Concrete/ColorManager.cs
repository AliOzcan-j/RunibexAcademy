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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result != null)
            {
                return result;
            }

            _colorDal.Add(entity);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }

            _colorDal.Delete(entity);
            return new SuccessResult();
        }

        [CacheAspect(typeof(DataResult<List<Color>>))]
        public IDataResult<List<Color>> GetAll(Expression<Func<Color, bool>> filter = null)
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAllWithoutTracker(filter));
        }

        [CacheAspect(typeof(DataResult<Color>))]
        public IDataResult<Color> GetByName(string name)
        {
            var result = _colorDal.Get(c => c.Name == name);
            if (result != null)
            {
                return new SuccessDataResult<Color>(result);
            }
            return new ErrorDataResult<Color>();
        }

        [CacheAspect(typeof(DataResult<Color>))]
        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Color>(result);
            }
            return new ErrorDataResult<Color>();
        }

        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color entity)
        {
            IResult result = BusinessRules.Run(CheckIfExists(entity.Name));

            if (result == null)
            {
                return result;
            }
            _colorDal.Update(entity);
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
