using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.Rental;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
    }
}
