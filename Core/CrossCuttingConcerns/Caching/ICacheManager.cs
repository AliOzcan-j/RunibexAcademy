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

        //T Get<T>(string key);
        //object Get(string key);
        //object Get(string key, Type type);
        //void Add(string key, object data, int duration, Type type);
        //void Add(string key, object data, int duration);
        //void Add(string key, object data, Type type);
        //void Add(string key, object data);
        //bool IfExists(string key);
        //void Remove(string key);
        //void RemoveByPattern(string pattern);
    }
}
