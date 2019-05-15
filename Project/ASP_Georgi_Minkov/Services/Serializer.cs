using ASP_Georgi_Minkov.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ASP_Georgi_Minkov.Services
{
    public static class SerializerMachine
    {
        static int index = 0;
        const string endFileLocation = "C://Users//GeorgiMinkov//source//repos//ASP_Georgi_Minkov//ASP_Georgi_Minkov//XML_XSD//Form_XML//XML_";
        static SerializerMachine()
        {
            DirectoryInfo directory = new DirectoryInfo(endFileLocation.Remove(endFileLocation.Length - 4));
            FileInfo[] files = directory.GetFiles("*.xml");

            index = files.Length;
        }
        public static Fotm deserializer(FileInfo file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Fotm));
            TextReader reader = new StreamReader(file.FullName);
            Fotm element = (Fotm)deserializer.Deserialize(reader);

            reader.Close();

            return element;
        }
        public static void serializer(Fotm element)
        {
            // Get Directory and continue from last file number
            XmlSerializer serializer = new XmlSerializer(typeof(Fotm));
            TextWriter writer = new StreamWriter(endFileLocation + (index++) + ".xml");

            serializer.Serialize(writer, element);

            writer.Close();
        }
    }
}