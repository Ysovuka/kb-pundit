using Newtonsoft.Json;

namespace Pundit.KnowledgeBase.WebCore
{
    public static class CastExtension
    {
        public static TOut Cast<TIn, TOut>(this TIn obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<TOut>(serialized);
        }
    }
}
