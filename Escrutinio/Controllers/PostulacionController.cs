using AutoMapper;
using Escrutinio.Database;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class PostulacionController : Controller
    {
        ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        public ActionResult Index()
        {
            var lista = ListaPostulaciones("SP_POSTULACION_INDEX");

            return View(lista);
        }

        internal List<PostulacionIndexModel> ListaPostulaciones(string storedProcedure)
        {
            List<PostulacionIndexModel> lista = db.Database.SqlQuery<PostulacionIndexModel>(storedProcedure).ToList<PostulacionIndexModel>();

            return lista;
        }

        public ActionResult Create()
        {
            ViewBag.Listas = db.LISTA.OrderBy(l => l.DESCRIPCION).ToList();
            ViewBag.Cargos = db.CARGO.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(POSTULACION postulacion)
        {
            if (ModelState.IsValid)
            {
                POSTULACION pos = db.POSTULACION.Where(p => p.CARGO == postulacion.CARGO
                    && p.LISTA == postulacion.LISTA).FirstOrDefault();

                if (pos == null)
                {
                    postulacion.ORID = Guid.NewGuid();
                    db.POSTULACION.Add(postulacion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Listas = db.LISTA.ToList();
            ViewBag.Cargos = db.CARGO.ToList();

            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            //POSTULACION pos = db.POSTULACION.Find(id);
            //PostulacionEditModel model = EntidadModelo(pos);

            //CANDIDATO can = db.CANDIDATO.Where(c => c.POSTULACION == pos.ORID).FirstOrDefault();

            //if (can != null)
            //{
            //    model.APELLIDO = can.APELLIDO;
            //    model.NOMBRE = can.NOMBRE;
            //}

            PostulacionEditModel model = new PostulacionEditModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PostulacionEditModel model)
        {
            POSTULACION pos = ModeloEntidad(model);

            CANDIDATO can = db.CANDIDATO.Where(c => c.POSTULACION == model.ORID).FirstOrDefault();

            if (can == null)
            {
                CANDIDATO can1 = new CANDIDATO();

                can1.ORID = Guid.NewGuid();
                can1.NOMBRE = model.NOMBRE;
                can1.APELLIDO = model.APELLIDO;
                can1.POSTULACION = model.ORID;

                db.CANDIDATO.Add(can1);
                db.SaveChanges();
            }
            else
            {
                can.NOMBRE = model.NOMBRE;
                can.APELLIDO = model.APELLIDO;

                db.Entry(can).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private PostulacionEditModel EntidadModelo(POSTULACION entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<POSTULACION, PostulacionEditModel>();
            });
            IMapper mapper = config.CreateMapper();
            PostulacionEditModel model = mapper.Map<POSTULACION, PostulacionEditModel>(entity);

            return model;
        }

        private POSTULACION ModeloEntidad(PostulacionEditModel model)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostulacionEditModel, POSTULACION>();
            });
            IMapper mapper = config.CreateMapper();
            POSTULACION entidad = mapper.Map<PostulacionEditModel, POSTULACION>(model);

            return entidad;
        }
    }
}
