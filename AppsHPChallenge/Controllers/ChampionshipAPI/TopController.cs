using AppsHPChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppsHPChallenge.Controllers.ChampionshipAPI
{
    public class TopController : ApiController
    {


        //GET api/championship/top
        /// <summary>
        /// Retrieves the top players of all championships.
        /// </summary>
        /// <param name="count">Sets the count of records to be retrieved.</param>
        /// <returns>>Returns the list of player names based on the count provided.</returns>
        public string Get(int count)
        {
            try
            {
                int defaultCount = 10;
                string result;
                List<string> bestPlayers = new List<string>();
                Ranking generalRanking = new Ranking();

                if (count > 0)
                    defaultCount = count;

                generalRanking.getBestPlayers(defaultCount);
                foreach (Player player in generalRanking.BestPlayers)
                {
                    bestPlayers.Add(player.Name);
                }

                result = System.Web.Helpers.Json.Encode(new { players = bestPlayers });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //POST api/championship/top
        /// <summary>
        /// Retrieves the top players of all championships.
        /// </summary>
        /// <param name="count">Sets the count of records to be retrieved.</param>
        /// <returns>Returns the list of player names based on the count provided.</returns>
        public string Post([FromBody]int count)
        {
            try
            {
                int defaultCount = 10;
                string result;
                List<string> bestPlayers = new List<string>();
                Ranking generalRanking = new Ranking();

                if (count > 0)
                    defaultCount = count;

                generalRanking.getBestPlayers(defaultCount);
                foreach (Player player in generalRanking.BestPlayers)
                {
                    bestPlayers.Add(player.Name);
                }

                result = System.Web.Helpers.Json.Encode(new { players = bestPlayers });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
