using AppsHPChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;


namespace AppsHPChallenge.Controllers.ChampionshipAPI
{
    public class NewController : ApiController
    {
        // GET api/championship/add
        /// <summary>
        /// Receives the championship data and computes it to identify the winner. The first and second place are stored into a database with their respective scores.
        /// </summary>
        /// <param name="data">The data of the championship following the bracketed array standard.</param>
        /// <returns>Returns the winner of the championship</returns>
        public string Get(string data)
        {
            try
            {
                Player[] players;
                Player firstPlace;
                string result;
                List<string> winnerDetails = new List<string>();
                Championship tournament = new Championship();
                Ranking generalRanking = new Ranking();

                players = tournament.getWinners(data);
                firstPlace = players.Where(player => player.Winner == true).First();
                generalRanking.addPlayerToRanking(players);
                winnerDetails.Add(firstPlace.Name);
                winnerDetails.Add(firstPlace.Strategy);

                result = System.Web.Helpers.Json.Encode(new { winner = winnerDetails });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST api/championship/add
        /// <summary>
        /// Receives the championship data and computes it to identify the winner. The first and second place are stored into a database with their respective scores.
        /// </summary>
        /// <param name="value">The data of the championship following the bracketed array standard.</param>
        /// <returns>Returns the winner of the championship</returns>
        public string Post([FromBody]string data)
        {
            try
            {
                Player[] players;
                Player firstPlace;
                string result;
                List<string> winnerDetails = new List<string>();
                Championship tournament = new Championship();
                Ranking generalRanking = new Ranking();

                players = tournament.getWinners(data);
                firstPlace = players.Where(player => player.Winner == true).First();
                generalRanking.addPlayerToRanking(players);
                winnerDetails.Add(firstPlace.Name);
                winnerDetails.Add(firstPlace.Strategy);

                result = System.Web.Helpers.Json.Encode(new { winner = winnerDetails });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

