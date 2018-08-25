using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Groups
    {
        [XmlElement("group")]
        public List<Group> groupsList = new List<Group>();
    }
}