using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : RedisConnect, ICacheManager
    {
        public void Add(string key, dynamic value, int duration, Type type)
        {
            if (value.Success)
            {
                string jsonType = JsonConvert.SerializeObject(value, type, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                });
                Database.ListRightPush(key, jsonType);
                Database.KeyExpire(key, TimeSpan.FromMinutes(duration));
                return;
            }
            else
            {
                value.Message = "Record doesnt exists";
                return;
            }
        }

        public T Get<T>(string key, Type type)
        {
            var jsonType = (T)JObject.Parse(Database.ListRange(key, -1).FirstOrDefault()).ToObject(type);
            return jsonType;
        }

        public object Get(string key, Type type)
        {
            var parse = Database.ListRange(key, 0, 1).ToList();
            dynamic jsonType = JObject.Parse(parse[0]).ToObject(type);
            return jsonType;
        }

        public bool IfExists(string key)
        {
            var result = Database.ListGetByIndex(key, 0).HasValue;
            return result;
        }

        public void Remove(string key)
        {
            Database.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)//IEntityServiceBase.Get
        {
            //var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            var server = Connection.GetServer(Connection.GetEndPoints()[0]);
            foreach (var key in server.Keys(pattern: "*"+pattern+"*"))
            {
                Remove(key.ToString());
            }
        }
    }
}
