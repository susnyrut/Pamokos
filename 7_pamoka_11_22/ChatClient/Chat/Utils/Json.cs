using Newtonsoft.Json;

namespace Chat.Utils
{
    static class Json
    {
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }

        public static T Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj, JsonSettings);
        }
    }
}
