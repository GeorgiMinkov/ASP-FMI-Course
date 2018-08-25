using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Track
    {
        [XmlAttribute("numberOfRaces")]
        public int numberOfRaces;
        public int id;
        public int firstRace;
        public float lapRecord;
        public string name;
        public int bestPilotId;
    }
}