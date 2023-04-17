using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Core.Console.Extensions
{
    public static class IDatabaseExtensions
    {
        public static bool StringSetAllServers(this IDatabase database, RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            return true;
        }
    }
}
