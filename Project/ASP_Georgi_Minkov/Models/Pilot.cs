using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Pilot
    {
        [XmlAttribute("number")]
        public int number { set; get; }
        public int id { set; get; }
        public string name { set; get; }
        public string nationality { set; get; }

        public Pilot()
        {
            this.number = 0;
            this.id = 0;
            this.name = "";
            this.nationality = "";
        }

        public Pilot(int number, int id, string name, string nationality)
        {
            this.number = number;
            this.id = id;
            this.name = name;
            this.nationality = nationality;
        }
    }
}