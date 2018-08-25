using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ASP_Georgi_Minkov.Services
{
    public class ValidateXmlUsingXsd
    {
        public static bool isValid(string xsdName, string xmlName) // give URI of files
        {
            bool errors = true;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdName);

            XDocument file = null;
            try
            {
                file = XDocument.Load(xmlName);
            } catch (FileNotFoundException ex)
            {
                return errors;
            }

            if (file != null)
            {
                file.Validate(schemas, (o, e) =>
                {
                    errors = false;
                }
                );
            }
            

            return errors;
        }
    }
}