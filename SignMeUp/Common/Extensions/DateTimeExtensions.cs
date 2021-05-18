using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToJsonString(this DateTime dt)
        {
            return JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(dt));
        }
    }
}
