using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Team
    {
        public int id { set; get; }
        public string name { set; get; }
        public string colour { set; get; }
        public int numberOfWins { set; get; }
        public int numberOfPolePositions { set; get; }
        public string teamChief { set; get; }
        public string technicalChief { set; get; }
        public int budget { set; get; }
        public string firsTeamEntry { set; get; }
        public string baseLocation { set; get; }
        public int numberOfRaces { set; get; }
        [XmlElement("pilots")]
        public Pilots pilots { set; get; }
        public string powerUnit { set; get; }
        public float fastestPitStop { set; get; }
        public string teamLogo { set; get; }
    }
}