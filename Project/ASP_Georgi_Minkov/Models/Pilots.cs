using System.Collections.Generic;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Pilots
    {
        [XmlElement("pilot")]
        public List<Pilot> pilotsList = new List<Pilot>();
    }
}