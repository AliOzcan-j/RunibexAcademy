using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;
        private Type _type;

        public CacheAspect(Type type,int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            _type = type;
        }

        public override void Intercept(IInvocation invocation)
        {
            var reflectType = invocation.Method.ReflectedType.Name;
            var methodName = string.Format($"{invocation.Method.ReflectedType.Namespace}.{reflectType.Remove(reflectType.Length-2)}.{invocation.Method.Name}");//runtime da, cachelenen metodun namespace ve ismini alır
            var arguments = invocation.Arguments.ToList();//metod parametre alıyorsa onları listeye ekler
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //metod ismi ve parametre, varsa, hepsini ekleyere bir key oluşturur
            //var type = invocation.Method.ReturnType.GenericTypeArguments.First();
            
            if (_cacheManager.IfExists(key))//bu veri cachelenmiş mi, hazırda varsa true
            {
                invocation.ReturnValue = _cacheManager.Get(key, _type);//var olan cachei veri tipine parse ederek geri dönderir dönderir
                return;
            }
            invocation.Proceed();//veri yok, metoda devam et
            _cacheManager.Add(key, invocation.ReturnValue, _duration, _type);//devamında cachele
        }
    }
}
