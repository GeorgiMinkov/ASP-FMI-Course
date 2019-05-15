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

        public Track()
        {
            this.numberOfRaces = 0;
            this.id = 0;
            this.firstRace = 0;
            this.lapRecord = 0.0f;
            this.name = "";
            this.bestPilotId = 0;
        }

        public Track(int numberOfRaces, int id, int firstRace, float lapRecord, string name, int bestPilotId)
        {
            this.numberOfRaces = numberOfRaces;
            this.id = id;
            this.firstRace = firstRace;
            this.lapRecord = lapRecord;
            this.name = name;
            this.bestPilotId = bestPilotId;
        }
    }
}