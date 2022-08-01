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

        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAllWithoutTracker());
        }

        public IDataResult<Color> GetByName(string name)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Name == name));
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id));
        }

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
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
