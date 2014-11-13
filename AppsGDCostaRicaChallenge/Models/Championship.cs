using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Apps_GD_Costa_Rica___Challenge.DAL;

namespace Apps_GD_Costa_Rica___Challenge.Models
{
    public class Championship
    {
        #region Attributes
        private string _firstPlace;

        public string FirstPlace
        {
            get { return _firstPlace; }
            set { _firstPlace = value; }
        }
        private string _secondPlace;

        public string SecondPlace
        {
            get { return _secondPlace; }
            set { _secondPlace = value; }
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Gets the best two players of the championship
        /// </summary>
        /// <param name="pDetails">Details of the brackets</param>
        /// <returns></returns>
        public Player[] getWinners(string pDetails)
        {
            try
            {
                XDocument XmlDocument;
                IEnumerable<XElement> nodes;
                byte[] encodedString;
                MemoryStream ms;
                Documents manageDocuments = new Documents();
                Player[] Winners = new Player[2];

                pDetails = manageDocuments.convertToXML(pDetails);
                encodedString = Encoding.UTF8.GetBytes(pDetails);
                ms = new MemoryStream(encodedString);
                ms.Flush();
                ms.Position = 0;

                XmlDocument = XDocument.Load(ms);
                nodes = XmlDocument.Elements("bracket").Elements("bracket");

                if (nodes != null && nodes.Count() > 0)
                {
                    if (nodes.Count() != 2)
                        throw new Exception("Error: Number of player per match is not equal to two");

                    else
                    {
                        Player[] WinnersFirstBracket = getWinners(nodes.ElementAt(0).ToString());
                        Player[] WinnersSecondBracket = getWinners(nodes.ElementAt(1).ToString());

                        Winners = playGame(WinnersFirstBracket.Where(item => item.Winner == true).First(), WinnersSecondBracket.Where(item => item.Winner == true).First());
                    }
                    
                }

                else
                {
                    nodes = XmlDocument.Elements("bracket").Elements("player");

                    if (nodes.Count() != 2)
                        throw new Exception("Error: Number of player per match is not equal to two");

                    else { 
                        for (int count = 0; count < nodes.Count(); count++)
                        {
                            IEnumerable<XElement> innerNodes;
                            innerNodes = nodes.ElementAt(count).Elements("playerinfo");
                            Winners[count] = new Player(innerNodes.ElementAt(0).Value, innerNodes.ElementAt(1).Value.ToUpper());
                        }
                        Winners = playGame(Winners[0], Winners[1]);
                    }
                }
                return Winners;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Plays a match between two players and set the winner flag to the winner.
        /// </summary>
        /// <param name="pFirstPlayer"></param>
        /// <param name="pSecondPlayer"></param>
        /// <returns></returns>
        public Player[] playGame(Player pFirstPlayer, Player pSecondPlayer)
        {
            try
            {
                Player[] Results = new Player[2];

                if (pFirstPlayer.Strategy == "R")
                {
                    if (pSecondPlayer.Strategy == "S" || pSecondPlayer.Strategy == "R")
                    {
                        pFirstPlayer.Winner = true;
                        pSecondPlayer.Winner = false;
                    }
                    else if(pSecondPlayer.Strategy == "P"){
                        pSecondPlayer.Winner = true;
                        pFirstPlayer.Winner = false;
                    }
                    else
                    {
                        throw new Exception(pSecondPlayer.Strategy + " has an invalid strategy");
                    }
                }
                else if (pFirstPlayer.Strategy == "S")
                {
                    if (pSecondPlayer.Strategy == "P" || pSecondPlayer.Strategy == "S")
                    {
                        pFirstPlayer.Winner = true;
                        pSecondPlayer.Winner = false;
                    }
                    else if (pSecondPlayer.Strategy == "R")
                    {
                        pSecondPlayer.Winner = true;
                        pFirstPlayer.Winner = false;
                    }
                    else
                    {
                        throw new Exception(pSecondPlayer.Name + " has an invalid strategy");
                    }
                }
                else if (pFirstPlayer.Strategy == "P")
                {
                    if (pSecondPlayer.Strategy == "R" || pSecondPlayer.Strategy == "P")
                    {
                        pFirstPlayer.Winner = true;
                        pSecondPlayer.Winner = false;
                    }
                    else if (pSecondPlayer.Strategy == "S")
                    {
                        pSecondPlayer.Winner = true;
                        pFirstPlayer.Winner = false;
                    }
                    else
                    {
                        throw new Exception(pSecondPlayer.Name + " has an invalid strategy");
                    }
                }
                else
                {
                    throw new Exception(pFirstPlayer.Name + " has an invalid strategy");
                }

                Results[0] = pFirstPlayer;
                Results[1] = pSecondPlayer;

                return Results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}