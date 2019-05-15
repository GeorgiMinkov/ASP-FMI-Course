using System.Collections.Generic;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Teams
    {
        [XmlElement("team")]
        public List<Team> teamsList = new List<Team>();
    }
}