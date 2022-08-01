using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business.Abstract
{
    public interface IEntityServiceAddon<T> where T : class, IEntity, new()
    {
        IDataResult<T>? GetByName(string name);
    }
}
