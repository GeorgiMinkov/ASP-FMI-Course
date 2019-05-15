using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ASP_Georgi_Minkov.Services;
using ASP_Georgi_Minkov.Models;
using System.IO;
using System.Collections.Specialized;
using System.Globalization;

namespace ASP_Georgi_Minkov.Controllers
{
    public class PagesController : Controller
    {
        public const string fileLocation = "C://Users//GeorgiMinkov//source//repos//ASP_Georgi_Minkov//ASP_Georgi_Minkov//XML_XSD//";
        // GET: Pages
        public ActionResult Load()
        {
            IList<FileManagment> fileManagment = new List<FileManagment>();
            //string fileName = fileLocation + "XML_FILE_{0}.xml";
            const string xsd = fileLocation + "asp_schema.xsd";

            DirectoryInfo directory = new DirectoryInfo(fileLocation);
            FileInfo[] files = directory.GetFiles("*.xml");

            foreach (FileInfo file in files)
            {
                FileManagment element = null;
                if (ValidateXmlUsingXsd.isValid(xsd, file.FullName))
                {
                    Fotm obj = SerializerMachine.deserializer(file);
                    
                    SqlConnector sql = new SqlConnector(obj);
                    bool isSaved = sql.saveToDb();
                    if (isSaved)
                    {
                        element = new FileManagment(file.Name, "Валиден", true, "Зареден", true);
                    }
                    else
                    {
                        element = new FileManagment(file.Name, "Валиден", true, "Отхвърлен - намира се в БД", false);
                    }
                }
                else
                {
                    element = new FileManagment(file.Name, "Невалиден", false, "Отхвърлен - невалиден", false);
                }

                fileManagment.Add(element);
            }
            
            return View(fileManagment);
        }

        public ActionResult Form()
        {            
            return View();
        }

        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveData()
        {
            NameValueCollection formData = this.Request.Form;

            Fotm root = new Fotm();

            try
            {
                // Get data for pilots
                Pilots pilots = new Pilots();
                Pilot pilot = null;

                string[] prefixes = { "firstPilot", "secondPilot" };
                foreach (string prefix in prefixes)
                {
                    pilot = new Pilot(Convert.ToInt32(formData[prefix + "Number"]), Convert.ToInt32(formData[prefix + "Id"]), formData[prefix + "Name"], formData[prefix + "Nationality"]);

                    pilots.pilotsList.Add(pilot);
                }

                // Get data for track
                Tracks tracks = new Tracks();
                Track track = new Track(Convert.ToInt32(formData["trackNumberOfRaces"]), Convert.ToInt32(formData["trackId"]), Convert.ToInt32(formData["firstRace"]),
                    System.Convert.ToSingle(formData["lapRecord"]), formData["trackName"], Convert.ToInt32(formData["bestPilotId"]));

                tracks.tracksList.Add(track);

                // Get data for team
                Teams teams = new Teams();
                Team team = new Team(Convert.ToInt32(formData["teamId"]), formData["teamName"], Convert.ToInt32(formData["teamTitles"]),
                    formData["colour"], Convert.ToInt32(formData["pointsEarned"]), Convert.ToInt32(formData["numberOfWins"]), Convert.ToInt32(formData["numberOfPolePosition"]),
                    formData["teamChief"], formData["technicalChief"], Convert.ToInt32(formData["budget"]), formData["firstTeamEntry"],
                    formData["baseLocation"], Convert.ToInt32(formData["numberOfRaces"]), pilots, formData["powerUnit"],
                    System.Convert.ToSingle(formData["fastestPitStop"]), formData["teamNationality"], "default.jpeg", formData["nickname"],
                    formData["group"], formData["currency"], formData["trackEntered"]);

                teams.teamsList.Add(team);

                root.teams = teams;
                root.tracks = tracks;
                root.groups = initGroup();

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата и нереализиране на XML файла";
                return View("About");
            }

            // INPUT DATA TO XML
            try
            {
                SerializerMachine.serializer(root);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата и нереализиране на XML файла";
                return View("About");
            }

            // INPUT DATA TO DB

            var directory = new DirectoryInfo("C://Users//GeorgiMinkov//source//repos//ASP_Georgi_Minkov//ASP_Georgi_Minkov//XML_XSD//Form_XML//");
            var myFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();


            var lastFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();

            bool check = false;
            if (ValidateXmlUsingXsd.isValid(fileLocation + "asp_schema.xsd", lastFile.FullName))
            {
                SqlConnector connector = new SqlConnector(root);
                check = connector.saveToDb();

                ViewBag.Message = (check) ? "Успешно запазени параметри в базата" : "Неуспешно запазване на елементите в базата";
            }
            else
            {
                ViewBag.Message = "Неуспешно запазване на елементите в базата - невалидно генериран XML";
            }


            return View("About");
        }
        
        private Groups initGroup()
        {
            Dictionary<int, string> values = new Dictionary<int, string>();

            values.Add(1, "Група 1 - Водещи отбори");
            values.Add(2, "Група 2 - Солидни отбори");
            values.Add(3, "Група 3 - Напредващи отбори");
            values.Add(4, "Група 4 - Отпадащи отбори");
            

            Groups groups = new Groups();
            Group group = null;
            for (int key = 1; key <= 4; ++key)
            {
                group = new Group("GR" + key, values[key]);

                groups.groupsList.Add(group);
            }

            return groups;
        }

    }
}