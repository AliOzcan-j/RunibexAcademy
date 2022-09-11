using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        void Add(string key, object value, int duration, Type type);
        bool IfExists(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        T Get<T>(string key, Type type);
        object Get(string key, Type type);
    }
}
