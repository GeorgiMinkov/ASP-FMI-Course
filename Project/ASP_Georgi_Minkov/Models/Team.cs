using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Team
    {
        public int id { set; get; }
        public string name { set; get; }
        public int titles { set; get; }
        public string colour { set; get; }
        public int pointsEarned { set; get; }
        public int numberOfWins { set; get; }
        public int numberOfPolePositions { set; get; }
        public string teamChief { set; get; }
        public string technicalChief { set; get; }
        public int budget { set; get; }
        public string firstTeamEntry { set; get; }
        public string baseLocation { set; get; }
        public int numberOfRaces { set; get; }
        [XmlElement("pilots")]
        public Pilots pilots { set; get; }
        public string powerUnit { set; get; }
        public float fastestPitStop { set; get; }
        [XmlAttribute("country")]
        public string country { set; get; }
        public string teamLogo { set; get; }
        [XmlAttribute("nickname")]
        public string nickname { set; get; }
        [XmlAttribute("group")]
        public string group { set; get; }
        [XmlAttribute("currency")]
        public string currency { set; get; }
        [XmlAttribute("trackEntered")]
        public string trackEntered { set; get; }

        public Team()
        {
            this.id = 0;
            this.name = "";
            this.titles = 0;
            this.colour = "";
            this.pointsEarned = 0;
            this.numberOfWins = 0;
            this.numberOfPolePositions = 0;
            this.teamChief = "";
            this.technicalChief = "";
            this.budget = 0;
            this.firstTeamEntry = "";
            this.baseLocation = "";
            this.numberOfRaces = 0;
            this.pilots = null;
            this.powerUnit = "";
            this.fastestPitStop = 0.0f;
            this.country = "";
            this.teamLogo = "";
            this.nickname = "";
            this.group = "";
            this.currency = "";
            this.trackEntered = "";
        }

        public Team(int id, string name, int titles, string colour, int pointsEarned, int numberOfWins, int numberOfPolePositions, string teamChief, string technicalChief, int budget, string firstTeamEntry, string baseLocation, int numberOfRaces, Pilots pilots, string powerUnit, float fastestPitStop, string country, string teamLogo, string nickname, string group, string currency, string trackEntered)
        {
            this.id = id;
            this.name = name;
            this.titles = titles;
            this.colour = colour;
            this.pointsEarned = pointsEarned;
            this.numberOfWins = numberOfWins;
            this.numberOfPolePositions = numberOfPolePositions;
            this.teamChief = teamChief;
            this.technicalChief = technicalChief;
            this.budget = budget;
            this.firstTeamEntry = firstTeamEntry;
            this.baseLocation = baseLocation;
            this.numberOfRaces = numberOfRaces;
            this.pilots = pilots;
            this.powerUnit = powerUnit;
            this.fastestPitStop = fastestPitStop;
            this.country = country;
            this.teamLogo = teamLogo;
            this.nickname = nickname;
            this.group = group;
            this.currency = currency;
            this.trackEntered = trackEntered;
        }
    }
}