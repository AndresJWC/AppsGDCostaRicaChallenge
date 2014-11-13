using AppsHPChallenge.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AppsHPChallenge.Models
{
    public class Ranking
    {
        #region Attributes
        private IList<Player> _BestPlayers = new List<Player>();

        public IList<Player> BestPlayers
        {
            get { return _BestPlayers; }
            set { _BestPlayers = value; }
        }
        #endregion

        #region Methods


        /// <summary>
        /// Upload a new tournament file to the server
        /// </summary>
        public void addNewTournament(HttpPostedFileBase pFile)
        {
            try
            {
                Championship newChampionshipDetails = new Championship();
                Documents manageDocuments = new Documents();

                if (pFile != null && pFile.ContentLength > 0)
                {
                    Player[] winners;
                    BinaryReader fileContentReader = new BinaryReader(pFile.InputStream);
                    byte[] fileContentByte = fileContentReader.ReadBytes((int)pFile.InputStream.Length);
                    string championshipInfo = System.Text.Encoding.UTF8.GetString(fileContentByte);

                    winners = newChampionshipDetails.getWinners(championshipInfo);
                    addPlayerToRanking(winners);
                    manageDocuments.saveDocument(pFile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Get the best players
        /// </summary>
        /// <param name="pCount">Amount of players retreived</param>
        /// <returns></returns>
        public void getBestPlayers(int pCount)
        {
            IEnumerable<XElement> bestPlayersXML;
            Documents manageDocuments = new Documents();
            bestPlayersXML = manageDocuments.getBestPlayers(10);

            foreach (var player in bestPlayersXML)
            {
                Player topTenPlayer = new Player(player.Element("Name").Value, int.Parse(player.Element("Points").Value));
                _BestPlayers.Add(topTenPlayer);
            }
        }

        /// <summary>
        /// Clears the current ranking
        /// </summary>
        public void resetRanking()
        {
            Documents manageDocuments = new Documents();
            manageDocuments.replaceRanking();
        }

        /// <summary>
        /// Adds/Uptades the given players to the ranking
        /// </summary>
        /// <param name="pPlayers">List of players to add to the ranking</param>
        public void addPlayerToRanking(Player[] pPlayers)
        {
            Documents manageDocuments = new Documents();
            foreach (Player player in pPlayers)
            {
                if (player.Winner)
                    manageDocuments.addPlayertoRanking(player.Name, 3);
                else
                    manageDocuments.addPlayertoRanking(player.Name, 1);
            }
        }

        /// <summary>
        /// Retreive all the championships files
        /// </summary>
        /// <returns>List with the path of all the championships files</returns>
        public List<string> getAllChampionshipsDocuments()
        {
            Documents manageDocuments = new Documents();
            return manageDocuments.getChampionshipsDocuments();
        }
        #endregion
    }
}