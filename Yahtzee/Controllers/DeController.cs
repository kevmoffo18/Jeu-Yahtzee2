using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yahtzee.Models;

namespace Yahtzee.Controllers
{
    public class DeController : Controller
    {
        private List<int> ListNumde; //Cette liste contient le numéro random entre 1 et 6 pour les faces du dé
        private List<string> ListNomDe; //Liste contenant le nom des des 5 dés
        private static int face1;
        private static int face2;
        private static int face3;
        private static int face4;
        private static int face5;
        private static int face6;
        private static int cptClik = 0; //Compteur de click
        private static int total; //Contient la somme total des faces à chaque tour.
        private bd_YahtzeeEntities db;

        public DeController()
        {
            ListNumde = new List<int>();
            ListNomDe = new List<string>();
            db = new bd_YahtzeeEntities();
        }

        // GET: De
        public ActionResult Index()
        {
            string id = Request.Form["D1"];
            ViewBag.ListNomDe = ReturnListDe();
            ViewBag.CptClick = cptClik;
            cptClik++;
            if (cptClik > 1)
            {
                renitialiserFace();
                SaveAllFaceDe();
            }


            return View();
        }

        private List<int> ReturnListFaceDe()
        {
            Random rd = new Random();
            for (int i = 1; i <= 5; i++)
            {
                int nb = rd.Next(1, 7);
                ListNumde.Add(nb);
                if (cptClik >= 1)
                {
                    CalculScore(nb);
                }
            }

            return ListNumde;
        }


        private List<string> ReturnListDe()
        {
            List<int> ListFaceDe = ReturnListFaceDe();
            int nbrDe = 1;
            foreach (int nb in ListFaceDe)
            {
                if (nbrDe > 6)
                {
                    break;
                }
                else
                {
                    ListNomDe.Add("D" + nbrDe + "/" + nb);
                }
                nbrDe++;
            }


            return ListNomDe;

        }

        //On reset la face du dé à chaque lancée
        private void renitialiserFace()
        {
            face1 = 0;
            face2 = 0;
            face3 = 0;
            face4 = 0;
            face5 = 0;
            face6 = 0;
        }
        private void CalculScore(int face)
        {

            switch (face)
            {
                case 1:
                    face1 += 1;
                    total += face1;
                    ViewBag.Face1 = face1;
                    break;
                case 2:
                    face2 += 2;
                    total += face2;
                    ViewBag.Face2 = face2;
                    break;
                case 3:
                    face3 += 3;
                    total += face3;
                    ViewBag.Face3 = face3;
                    break;
                case 4:
                    face4 += 4;
                    total += face4;
                    ViewBag.Face4 = face4;
                    break;
                case 5:
                    face5 += 5;
                    total += total;
                    ViewBag.Face5 = face5;
                    break;
                case 6:
                    face6 += 6;
                    total += face6;
                    ViewBag.Face6 = face6;
                    break;

            }

            ViewBag.Total = total;
        }

        private void SaveAllFaceDe()
        {
            foreach (string de in ListNomDe)
            {
                tblDe q = (from d in db.tblDe where d.nomDe == de.Substring(0, 2) select d).FirstOrDefault();
                SaveFaceParDe(q, Convert.ToInt32(de.Substring(3, 1)));
            }

        }

        //Enregistrele nombre de lancé pour chaque dé.
        private void SaveFaceParDe(tblDe de, int nbLance)
        {
            tblFace dbFace = new tblFace { idDe = de.idDe, nbLancee = nbLance };
            de.tblFace.Add(dbFace);
            db.SaveChanges();
        }
    }
}