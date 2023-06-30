using Escrutinio.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class ListaController : Controller
    {
        ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        public ActionResult Index()
        {
            List<LISTA> lista = db.LISTA.OrderBy(l => l.ORDEN).ToList();

            return View(lista);
        }

        public ActionResult Create()
        {
            ViewBag.Agrupaciones = db.AGRUPACION.OrderBy(a => a.DESCRIPCION).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LISTA lista)
        {
            if (ModelState.IsValid)
            {
                lista.ORID = Guid.NewGuid();
                db.LISTA.Add(lista);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}