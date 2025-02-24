using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace xSdk.Extensions.Links
{
    public class Link
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlAttribute("method")]
        public string Method { get; set; }

        [XmlAttribute("rel")]
        [JsonIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public string Rel { get; set; }
    }
}
