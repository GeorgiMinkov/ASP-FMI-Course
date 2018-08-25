using System.Collections.Generic;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Tracks
    {
        [XmlElement("track")]
        public List<Track> tracksList = new List<Track>();
    }
}