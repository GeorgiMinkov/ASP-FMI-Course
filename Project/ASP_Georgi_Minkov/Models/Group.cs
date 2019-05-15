using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Models
{
    public class Group
    {
        [XmlAttribute("groupCode")]
        public string groupCode { set; get; }
        public string text { set; get; }

        static public bool isValidGroupCode(string groupCode)
        {
            ISet<string> codes = new HashSet<string>();
            for (int index = 0; index < 4;)
            {
                codes.Add("GR" + index);
            }

            return codes.Contains(groupCode);
        }

        public Group()
        {
            this.groupCode = "";
            this.text = "";
        }
        public Group(string groupCode, string text)
        {
            this.groupCode = groupCode;
            this.text = text;
        }
    }
}