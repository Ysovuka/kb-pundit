using Newtonsoft.Json;

namespace Pundit.Interlude
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
