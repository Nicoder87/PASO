using Escrutinio.Database;
using Escrutinio.Filters;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class PASOController : Controller
    {
        private ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        /// MÉTODOS DE VISTAS ///

        [AuthorizeRule]
        public ActionResult Index()
        {
            var model = new TodosModel();

            ViewBag.Tipos = db.TIPO_VOTO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Circuitos = db.CIRCUITO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Escuelas = db.ESCUELA.OrderBy(c => c.DESCRIPCION).ToList();

            model.REFERENCIA = ListaReferencias("SP_TOTALES_REFERENCIA @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);

            model.VOTOS = new VotosModel();
            model.VOTOS.OTROS_VOTOS = ListaOtrosVotos("SP_TOTALES_VOTOS @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);
            model.VOTOS.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_Loni2 @AGRUPACION, @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);
            //model.VOTOS.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_VIEJO");
            model.VOTOS.TIPO = null;

            return View(model);
        }

        [AuthorizeRule]
        public ActionResult IndexMovil()
        {
            var model = new TodosModel();

            ViewBag.Tipos = db.TIPO_VOTO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Circuitos = db.CIRCUITO.OrderBy(c => c.CODIGO).ToList();
            ViewBag.Escuelas = db.ESCUELA.OrderBy(c => c.DESCRIPCION).ToList();

            model.REFERENCIA = ListaReferencias("SP_TOTALES_REFERENCIA @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);

            model.VOTOS = new VotosModel();
            model.VOTOS.OTROS_VOTOS = ListaOtrosVotos("SP_TOTALES_VOTOS @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);
            model.VOTOS.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_Loni2 @AGRUPACION, @ESCUELA, @CIRCUITO, @EXTRANJERO", null, null, null);
            //model.VOTOS.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_VIEJO");

            return View(model);
        }

        /// VISTAS PARCIALES ///

        public ActionResult Referencias(Nullable<Guid> circuito, Nullable<Guid> escuela, Nullable<bool> extranjero, string v)
        {
            string parcial = String.Empty;
            List<TotalesReferenciaModel> lista = ListaReferencias
                ("SP_TOTALES_REFERENCIA @ESCUELA, @CIRCUITO, @EXTRANJERO",
                circuito, escuela, extranjero);

            if (v == "vm") { parcial = "_vmpReferencias"; }
            else { parcial = "_pcpReferencias"; }

            return PartialView(parcial, lista);
        }

        public ActionResult Votos(Nullable<Guid> circuito, Nullable<Guid> escuela, Nullable<bool> extranjero, string v)
        {
            string parcial = String.Empty;

            VotosModel vm = new VotosModel();

            vm.OTROS_VOTOS = ListaOtrosVotos
                ("SP_TOTALES_VOTOS @ESCUELA, @CIRCUITO, @EXTRANJERO",
                circuito, escuela, extranjero);

            vm.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_Loni2 @AGRUPACION, @ESCUELA, @CIRCUITO, @EXTRANJERO",
                circuito, escuela, extranjero);


            //vm.LISTAS_COMPLETAS = ListaListasCompletas("SP_LISTAS_COMPLETAS_VIEJO");

            if (Convert.ToBoolean(extranjero))
            {
                vm.TIPO = true;
            }
            if (Convert.ToBoolean(extranjero == false))
            {
                vm.TIPO = false;
            }

            if (v == "vm") { parcial = "_vmpVotos"; }
            else { parcial = "_pcpVotos"; }

            return PartialView(parcial, vm);
        }

        /// LISTAS ///

        public List<TotalesReferenciaModel> ListaReferencias(string storedProcedure, Nullable<Guid> c, Nullable<Guid> e, Nullable<bool> ex)
        {
            var cir = new SqlParameter();
            var esc = new SqlParameter();
            var ext = new SqlParameter();

            if (c == null) { cir = new SqlParameter("@CIRCUITO", DBNull.Value); }
            else { cir = new SqlParameter("@CIRCUITO", c); };

            if (e == null) { esc = new SqlParameter("@ESCUELA", DBNull.Value); }
            else { esc = new SqlParameter("@ESCUELA", e); };

            if (ex == null) { ext = new SqlParameter("@EXTRANJERO", DBNull.Value); }
            else { ext = new SqlParameter("@EXTRANJERO", ex); };

            List<TotalesReferenciaModel> lista = db.Database.SqlQuery<TotalesReferenciaModel>
                (storedProcedure, esc, cir, ext).ToList<TotalesReferenciaModel>();

            return lista;
        }

        public List<TotalesOtrosVotosModel> ListaOtrosVotos(string storedProcedure, Nullable<Guid> c, Nullable<Guid> e, Nullable<bool> ex)
        {
            var cir = new SqlParameter();
            var esc = new SqlParameter();
            var ext = new SqlParameter();

            if (c == null) { cir = new SqlParameter("@CIRCUITO", DBNull.Value); }
            else { cir = new SqlParameter("@CIRCUITO", c); };

            if (e == null) { esc = new SqlParameter("@ESCUELA", DBNull.Value); }
            else { esc = new SqlParameter("@ESCUELA", e); };

            if (ex == null) { ext = new SqlParameter("@EXTRANJERO", DBNull.Value); }
            else { ext = new SqlParameter("@EXTRANJERO", ex); };

            List<TotalesOtrosVotosModel> lista = db.Database.SqlQuery<TotalesOtrosVotosModel>
                (storedProcedure, esc, cir, ext).ToList<TotalesOtrosVotosModel>();

            return lista;
        }

        public List<ListasCompletasModel> ListaListasCompletas(string storedProcedure, Nullable<Guid> c, Nullable<Guid> e, Nullable<bool> ex)
        {
            var cir = new SqlParameter();
            var esc = new SqlParameter();
            var ext = new SqlParameter();

            var agr = new SqlParameter("@AGRUPACION", Guid.Parse("a102ae4c-22a8-4ec3-a82e-cdd051e32e52"));

            if (c == null) { cir = new SqlParameter("@CIRCUITO", DBNull.Value); }
            else { cir = new SqlParameter("@CIRCUITO", c); };

            if (e == null) { esc = new SqlParameter("@ESCUELA", DBNull.Value); }
            else { esc = new SqlParameter("@ESCUELA", e); };

            if (ex == null) { ext = new SqlParameter("@EXTRANJERO", DBNull.Value); }
            else { ext = new SqlParameter("@EXTRANJERO", ex); };

            List<ListasCompletasModel> lista = db.Database.SqlQuery<ListasCompletasModel>
                (storedProcedure, agr, esc, cir, ext).ToList<ListasCompletasModel>();

            return lista;
        }

        //public List<ListasCompletasModel> ListaListasCompletas(string storedProcedure)
        //{
        //    List<ListasCompletasModel> lista = db.Database.SqlQuery<ListasCompletasModel>
        //        (storedProcedure).ToList<ListasCompletasModel>();

        //    return lista;
        //}

        /// COMBOS ///

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
                    escuelas = db.ESCUELA.Where(e => e.CIRCUITO == IdCircuito)
                        .OrderBy(e => e.DESCRIPCION).ToList();
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
                    escuelas_nacionales = db.vwEscuelasNacionales.Where(e => e.CIRCUITO == IdCircuito)
                        .OrderBy(e => e.DESCRIPCION).ToList();
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
                    escuelas_extranjeras = db.vwEscuelasExtranjeras.Where(e => e.CIRCUITO == IdCircuito)
                        .OrderBy(e => e.DESCRIPCION).ToList();
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
