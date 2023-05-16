using Newtonsoft.Json;

namespace Core.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
