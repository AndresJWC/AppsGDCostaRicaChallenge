using Apps_GD_Costa_Rica___Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps_GD_Costa_Rica___Challenge.Controllers
{
    public class InformationController : Controller
    {
        //
        // GET: /Information/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Assumptions() {
            return View();
        }

        public ActionResult Downloads() {
            Ranking generalRanking = new Ranking();
            IList<string> documents = generalRanking.getAllChampionshipsDocuments();
            return View(documents); 
        }

        public FileResult Download(string ImageName)
        {
            return File(ImageName, System.Net.Mime.MediaTypeNames.Application.Octet, ImageName.Split('\\').Last());
        }
    }
}
