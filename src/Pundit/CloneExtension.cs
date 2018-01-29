using Newtonsoft.Json;

namespace Pundit
{
    public static class CloneExtension
    {
        public static T Clone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
