using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace xSdk.Extensions.Links
{
    [JsonConverter(typeof(LinkCollectionConverter))]
    public class LinkCollection : IEnumerable<Link>
    {
        private readonly Dictionary<string, Link> links = new Dictionary<string, Link>();

        public int Count => links.Count;

        public void Add(Link link) => links.Add(link.Name, link);

        public bool ContainsKey(string key) => links.ContainsKey(key);

        public IEnumerator<Link> GetEnumerator() => links.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Link this[string name]
        {
            get => links[name];
            set => links[name] = value;
        }
    }
}
