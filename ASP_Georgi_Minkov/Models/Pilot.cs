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
    }
}