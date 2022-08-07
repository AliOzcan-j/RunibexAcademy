using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICarImageDal : IEntityRepository<CarImage>
    {
        public List<CarImageListDto> GetCarImageList(Expression<Func<CarImageListDto, bool>> filter = null);
    }
}
