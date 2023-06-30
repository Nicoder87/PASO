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
    public class CircuitosEscrutadosController : Controller
    {
        ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        public ActionResult Index()
        {
            var lista = ListaPostulaciones("SP_CIRCUITOS_ESCRUTADOS @CIRCUITO", null);

            return View(lista);
        }

        internal List<CircuitosEscrutadosIndexModel> ListaPostulaciones(string storedProcedure, Nullable<Guid> c)//, Nullable<Guid> e, Nullable<bool> ex)
        {
            var cir = new SqlParameter();

            if (c == null) { cir = new SqlParameter("@CIRCUITO", DBNull.Value); } else { cir = new SqlParameter("@CIRCUITO", c); };

            List<CircuitosEscrutadosIndexModel> lista = db.Database.SqlQuery<CircuitosEscrutadosIndexModel>(storedProcedure, cir).ToList<CircuitosEscrutadosIndexModel>();

            return lista;
        }


        public ActionResult ActualizarDatos(Nullable<Guid> circuito)//, Nullable<Guid> escuela, Nullable<bool> extranjero)
        {
            var lista = ListaPostulaciones("SP_CIRCUITOS_ESCRUTADOS @CIRCUITO", null);

            return PartialView("_Lista", lista);

        }
    }
}