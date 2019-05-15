using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;
using System.Xml.XmlConfiguration;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    [Serializable, XmlRoot("fotm")]
    public class Fotm
    {
        [XmlElement("teams")]
        public Teams teams { set; get; }

        [XmlElement("groups")]
        public Groups groups { set; get; }

        [XmlElement("tracks")]
        public Tracks tracks { set; get; }

    }
}