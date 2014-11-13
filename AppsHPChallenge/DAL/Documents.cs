using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace AppsHPChallenge.DAL
{
    public class Documents
    {
        #region Methods
        /// <summary>
        /// Gets all the content of the championships files
        /// </summary>
        /// <returns></returns>
        public List<string> getChampionshipsDocuments()
        {
            try
            {
                string path = @"~/Documents";
                string[] pdfFiles = Directory.GetFiles(HttpContext.Current.Server.MapPath(path), "*.pdf");
                string[] txtFiles = Directory.GetFiles(HttpContext.Current.Server.MapPath(path), "*.txt");
                List<string> allFiles = new List<string>();

                allFiles.AddRange(pdfFiles);
                allFiles.AddRange(txtFiles);

                return allFiles;
            }

            catch
            {
                throw new Exception("Error reading the documents");
            }
        }

        /// <summary>
        /// Convert the chamionship list into XML
        /// </summary>
        /// <param name="pDetails">Content of the document</param>
        /// <returns></returns>
        public string convertToXML(string pDetails)
        {
            try
            {
                string Details;

                Details = Regex.Replace(pDetails, @"\n|\t|\r| ", "");
                Details = Regex.Replace(Details, ",\"", @"<playerinfo>");
                Details = Regex.Replace(Details, @"\[" + "\"", @"[<playerinfo>");
                Details = Regex.Replace(Details, "\"", @"</playerinfo>");
                Details = Regex.Replace(Details, "\"" + @"\]", @"</playerinfo>]");
                Details = Regex.Replace(Details, @"\[<playerinfo>", @"<player><playerinfo>");
                Details = Regex.Replace(Details, @",<playerinfo>", @",<player><playerinfo>");
                Details = Regex.Replace(Details, @"</playerinfo>\],|</playerinfo>\]", @"</playerinfo></player>");
                Details = Regex.Replace(Details, @"\[", @"<bracket>");
                Details = Regex.Replace(Details, @"\],|\]", @"</bracket>");

                return Details;
            }

            catch
            {
                throw new Exception("Error converting text documento to XML");
            }
        }

        /// <summary>
        /// Save the given document into the server
        /// </summary>
        /// <param name="pFile"></param>
        public void saveDocument(HttpPostedFileBase pFile)
        {
            string fileName = Path.GetFileName(pFile.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Documents/"), fileName);
            pFile.SaveAs(path);
        }

        /// <summary>
        /// Adds/Updates a player into the Ranking
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pPoints"></param>
        public void addPlayertoRanking(string pName, int pPoints)
        {
            List<string> tournamentsInfo = new List<string>();
            string rankingFile = getRankingFile();
            XDocument xmlDocument;

            using (StreamReader sr = new StreamReader(rankingFile))
            {
                IEnumerable<XElement> nodes;

                xmlDocument = XDocument.Load(sr);
                nodes = xmlDocument.Element("Ranking").Elements("Player").Where(element => element.Element("Name").Value == pName);


                if (nodes != null && nodes.Count() > 0)
                {
                    nodes.First().Element("Points").SetValue(int.Parse(nodes.First().Element("Points").Value) + pPoints);
                }
                else
                {
                    xmlDocument.Element("Ranking").Add(
                        new XElement("Player",
                            new XElement("Name", pName),
                            new XElement("Points", pPoints)
                            )
                    );
                }
            }
            xmlDocument.Save(rankingFile);
        }


        /// <summary>
        /// Clears the ranking document
        /// </summary>
        public void replaceRanking()
        {
            List<string> tournamentsInfo = new List<string>();
            string rankingFile = getRankingFile();
            XDocument xmlDocument;

            using (StreamReader sr = new StreamReader(rankingFile))
            {
                xmlDocument = XDocument.Load(sr);
                xmlDocument.Root.RemoveNodes();
            }
            xmlDocument.Save(rankingFile);
        }

        /// <summary>
        /// Get the best players
        /// </summary>
        /// <param name="pCount">Number of players retrieved</param>
        /// <returns></returns>
        public IEnumerable<XElement> getBestPlayers(int pCount)
        {
            IEnumerable<XElement> bestPlayers;
            XDocument xmlDocument;
            string rankingFile = getRankingFile();

            using (StreamReader sr = new StreamReader(rankingFile))
            {
                xmlDocument = XDocument.Load(sr);

                bestPlayers = xmlDocument.Descendants("Player").OrderByDescending(node => int.Parse(node.Element("Points").Value)).Take(pCount);
            }

            return bestPlayers;
        }

        /// <summary>
        /// Gets the ranking file full path
        /// </summary>
        /// <returns></returns>
        public string getRankingFile()
        {
            try
            {
                string path;
                string fileName;
                string[] files;

                path = HttpContext.Current.Server.MapPath(@"~/Documents");
                fileName = "Ranking.xml";
                files = Directory.GetFiles(path, fileName);

                return files[0];
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion
    }
}