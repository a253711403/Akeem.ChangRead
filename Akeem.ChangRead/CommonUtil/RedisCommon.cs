using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public class RedisCommon
    {

        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        public RedisCommon()
        {
            var connection = "127.0.0.1:6379";
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
        }

        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, TimeSpan? expiry= null)
        {
            return db.StringSet(key, value, expiry: expiry);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return db.StringGet(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }
    }
}
