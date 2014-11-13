using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppsHPChallenge.Models;
using System.IO;

namespace AppsHPChallenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Ranking generalRanking = new Ranking();
            generalRanking.getBestPlayers(10);
            return View(generalRanking.BestPlayers);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            Ranking generalRanking = new Ranking();
            if (file != null && file.ContentLength > 0)
                try
                {
                    generalRanking.addNewTournament(file);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            generalRanking.getBestPlayers(10);
            return View(generalRanking.BestPlayers);
        }

        [HttpPost]
        public ActionResult ResetRanking()
        {
            Ranking generalRanking;

            generalRanking = new Ranking();
            generalRanking.resetRanking();
            return RedirectToAction("Index", "Home");
        }
    }
}
