using Apps_GD_Costa_Rica___Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace Apps_GD_Costa_Rica___Challenge.Controllers.Championship_API
{
    public class ResultsController : ApiController
    {
        // GET api/championship/result
        /// <summary>
        /// Stores the first and second place of a tournament, each user is stored with their respective scores.
        /// </summary>
        /// <param name="first">The name of the winner of the championship.</param>
        /// <param name="second">The name of the second place of the championship.</param>
        /// <returns>Returns the operation status if successfully.</returns>
        public string Get(string first, string second)
        {
            try
            {
                string result;
                Ranking generalRanking = new Ranking();
                Player[] players = { new Player(first, 3), new Player(second, 1) };
                
                generalRanking.addPlayerToRanking(players);
                result = Json.Encode(new { status = "success" });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST api/championship/result
        /// <summary>
        /// Stores the first and second place of a tournament, each user is stored with their respective scores.
        /// </summary>
        /// <param name="first">The name of the winner of the championship.</param>
        /// <param name="second">The name of the second place of the championship.</param>
        /// <returns>Returns the operation status if successfully.</returns>
        public string Post([FromBody]string first, [FromBody]string second)
        {
            try
            {
                Ranking generalRanking = new Ranking();
                Player[] players = { new Player(first, 3), new Player(second, 1) };

                generalRanking.addPlayerToRanking(players);
                return "{status: 'success'}";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
