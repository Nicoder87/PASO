using Escrutinio.Database;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class MesasFaltantesController : Controller
    {
        ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        public ActionResult Index()
        {
            ViewBag.Tipos = db.TIPO_VOTO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Circuitos = db.CIRCUITO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Escuelas = db.ESCUELA.OrderBy(c => c.DESCRIPCION).ToList();

            var lista = ListaMesasFaltantes("SP_MESAS_NO_ESCRUTADAS @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);

            ViewBag.Cantidad = lista.Count;

            return View(lista);
        }

        public ActionResult ActualizarDatos(Nullable<Guid> circuito, Nullable<Guid> escuela, Nullable<bool> extranjero)
        {
            var lista = ListaMesasFaltantes("SP_MESAS_NO_ESCRUTADAS  @ESCUELA, @CIRCUITO, @EXTRANJERO", circuito, escuela, extranjero);

            ViewBag.Cantidad = lista.Count;

            return PartialView("_Lista", lista);
        }

        internal List<MesasFaltantesIndexModel> ListaMesasFaltantes(string storedProcedure, Nullable<Guid> c, Nullable<Guid> e, Nullable<bool> ex)
        {
            var cir = new SqlParameter();
            var esc = new SqlParameter();
            var ext = new SqlParameter();

            if (c == null) { cir = new SqlParameter("@CIRCUITO", DBNull.Value); } else { cir = new SqlParameter("@CIRCUITO", c); };
            if (e == null) { esc = new SqlParameter("@ESCUELA", DBNull.Value); } else { esc = new SqlParameter("@ESCUELA", e); };
            if (ex == null) { ext = new SqlParameter("@EXTRANJERO", DBNull.Value); } else { ext = new SqlParameter("@EXTRANJERO", ex); };

            List<MesasFaltantesIndexModel> lista = db.Database.SqlQuery<MesasFaltantesIndexModel>(storedProcedure, esc, cir, ext).ToList<MesasFaltantesIndexModel>();

            return lista;
        }

        public class Esc
        {
            public Guid ORID { get; set; }
            public string DESCRIPCION { get; set; }
        }

        public class Cir
        {
            public Guid ORID { get; set; }
            public string DESCRIPCION { get; set; }
        }

        public JsonResult GetEscuelas(string tipo, string circuito)
        {
            List<ESCUELA> escuelas = new List<ESCUELA>();
            var escuelas_nacionales = new List<vwEscuelasNacionales>();
            var escuelas_extranjeras = new List<vwEscuelasExtranjeras>();
            Guid IdCircuito = new Guid();

            if (circuito != String.Empty)
            {
                IdCircuito = Guid.Parse(circuito);
            }

            if (tipo == "0001")
            {
                if (IdCircuito == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    escuelas = db.ESCUELA.OrderBy(e => e.DESCRIPCION).ToList();
                }
                else
                {
                    escuelas = db.ESCUELA.Where(e => e.CIRCUITO == IdCircuito).OrderBy(e => e.DESCRIPCION).ToList();
                }
            }
            else if (tipo == "0002")
            {
                if (IdCircuito == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    escuelas_nacionales = db.vwEscuelasNacionales.OrderBy(e => e.DESCRIPCION).ToList();
                }
                else
                {
                    escuelas_nacionales = db.vwEscuelasNacionales.Where(e => e.CIRCUITO == IdCircuito).OrderBy(e => e.DESCRIPCION).ToList();
                }
            }
            else
            {
                if (IdCircuito == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    escuelas_extranjeras = db.vwEscuelasExtranjeras.OrderBy(e => e.DESCRIPCION).ToList();
                }
                else
                {
                    escuelas_extranjeras = db.vwEscuelasExtranjeras.Where(e => e.CIRCUITO == IdCircuito).OrderBy(e => e.DESCRIPCION).ToList();
                }
            }

            ////

            List<Esc> select_escuelas = new List<Esc>();

            if (tipo == "0001")
            {
                foreach (var item in escuelas)
                {
                    select_escuelas.Add(new Esc { ORID = item.ORID, DESCRIPCION = item.DESCRIPCION });
                }
            }
            else if (tipo == "0002")
            {
                foreach (var item in escuelas_nacionales)
                {
                    select_escuelas.Add(new Esc { ORID = item.ORID, DESCRIPCION = item.DESCRIPCION });
                }
            }
            else
            {
                foreach (var item in escuelas_extranjeras)
                {
                    select_escuelas.Add(new Esc { ORID = item.ORID, DESCRIPCION = item.DESCRIPCION });
                }
            }

            return Json(new { data = select_escuelas }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCircuitos(string tipo)
        {
            List<CIRCUITO> circuitos = new List<CIRCUITO>();
            var circuitos2 = new List<vwCircuitosExtranjeros>();

            if (tipo == "0003") // EXTRANJEROS
            {
                circuitos2 = db.vwCircuitosExtranjeros.ToList();
            }
            else
            {
                circuitos = db.CIRCUITO.OrderBy(c => c.DESCRIPCION).ToList();
            }

            List<Cir> select_circuitos = new List<Cir>();

            if (tipo == "0003") // EXTRANJEROS
            {
                foreach (var item in circuitos2)
                {
                    select_circuitos.Add(new Cir { ORID = item.ORID, DESCRIPCION = item.DESCRIPCION });
                }
            }
            else
            {
                foreach (var item in circuitos)
                {
                    select_circuitos.Add(new Cir { ORID = item.ORID, DESCRIPCION = item.DESCRIPCION });
                }
            }

            return Json(new { data = select_circuitos }, JsonRequestBehavior.AllowGet);
        }

    }
}
