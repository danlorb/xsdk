using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace xSdk.Extensions.Links
{
    public abstract class LinkContainer : ILinkContainer
    {
        [XmlElement("link")]
        [JsonProperty(PropertyName = "_links")]
        public LinkCollection Links { get; set; } = new LinkCollection();

        public void Add(Link link)
        {
            Links.Add(link);
        }
    }
}
