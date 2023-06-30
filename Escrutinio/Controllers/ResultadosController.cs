using Escrutinio.Models;
using Escrutinio.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using Escrutinio.Filters;

namespace Salud.Controllers
{
    public class ResultadosController : Controller
    {
        private ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        String berraco = "";
        String puntos = "";
        String cucha = "";
        int total = 0;
        string t = String.Empty;

        [AuthorizeRule]
        public ActionResult CargoLista(CargoListaModel model)
        {
            if (model.IdCargo == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                CARGO cargo = db.CARGO.Where(c => c.CODIGO == "0001").FirstOrDefault(); // INTENDENTE POR DEFECTO
                model.IdCargo = cargo.ORID;
            }

            ViewBag.Cargos = db.CARGO.ToList();
            TotalesCargoLista(model);

            return View();
        }

        [AuthorizeRule]
        public ActionResult CargoCircuito(CargoCircuitoModel model)
        {
            if (model.IdCargo == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                CARGO cargo = db.CARGO.Where(c => c.CODIGO == "0001").FirstOrDefault(); // INTENDENTE POR DEFECTO
                model.IdCargo = cargo.ORID;
            }

            ViewBag.Cargos = db.CARGO.ToList();
            TotalesCargoCircuito(model);

            return View();
        }

        [AuthorizeRule]
        public ActionResult Concejales()
        {
            try
            {
                List<ConcejalesModel> lista = db.Database.SqlQuery<ConcejalesModel>("SP_CONCEJALESxLISTAS_FatBased").ToList<ConcejalesModel>();
                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// </summary> ///

        private void TotalesCargoLista(CargoListaModel model)
        {
            var resultados = TotalCargoLista("SP_TOPLISTASxCARGO @CARGO", model.IdCargo);
            Blanqueo();

            foreach (var item in resultados)
            {
                total = total + item.VOTOS;
            }

            ViewBag.Total = total;

            puntos += "<div class=\"chart-legend clearfix\" style=\"padding-top:10px;font-size:15px;\">";
            foreach (var item in resultados)
            {
                if (item.VOTOS != 0)
                {
                    if (item.CANDIDATO != null)
                    {
                        item.CANDIDATO = "/ " + item.CANDIDATO;
                    }
                    item.PORCENTAJE_D = item.PORCENTAJE_D.Replace(",", ".");

                    berraco += "{ value : " + item.PORCENTAJE_D + ", color : '" + item.COLOR
                        + "', highlight :'" + item.COLOR + "', label : '" + item.LISTA + "' }, ";

                    puntos += "<div style=\"padding-top:10px\"><i class=\"fa fa-bookmark\" style=\"color:"
                             + item.COLOR + ";padding-right:5px;\"></i><span style=\"font-weight:800\">" + item.LISTA + " " + item.CANDIDATO + "</span>" +
                        "<h4>" + item.PORCENTAJE_D + " % - " + item.VOTOS + " votos </h4></div>";
                }
            }
            puntos += "</div>";

            if (berraco.Length > 0)
            {
                berraco = berraco.Substring(0, berraco.Length - 2);
                ViewBag.DatosTotalPorCargo = berraco;
                ViewBag.TotalPorCargoPuntos = puntos;
            }
        }

        private void TotalesCargoCircuito(CargoCircuitoModel model)
        {
            var resultados = TotalCargoCircuito("SP_CIRCUITOSxCARGO @CARGO", model.IdCargo);

            if (resultados.Count != 26)
            {
                ViewBag.Info = "PARA VISUALIZAR ESTE GRÁFICO DEBE HABER CARGADA AL MENOS UNA MESA POR CIRCUITO";
                return;
            }

            ViewBag.Info = string.Empty;

            String primero = String.Empty;
            String segundo = String.Empty;

            foreach (var item in resultados)
            {
                if (item.NUMERO == 18)
                {
                    primero += item.PORCENTAJE_D + ",";
                }
                if (item.NUMERO == 16)
                {
                    segundo += item.PORCENTAJE_D + ",";
                }
            }

            primero = primero.Substring(0, primero.Length - 1);
            segundo = segundo.Substring(0, segundo.Length - 1);

            ViewBag.DatosCargoCircuito = "{labels:[\'583\',\'584\',\'585\',\'586\',\'587\',\'588\',\'589\',\'590\', " +
                "\'590A\',\'591\',\'592\',\'593\',\'593A\']," +
                "datasets: [{label: \'Electronics\', fillColor: \'#0000FF\', " +
                "data: ["+ primero +"]}," +
                "{label:\'Digital Goods\',fillColor:\'#FCE903\',data:" +
                "[" + segundo + "]}]}";
        }

        public List<CargoListaModel> TotalCargoLista(string SP, Guid ca)
        {
            var cargo = new SqlParameter("@CARGO", ca);

            try
            {
                List<CargoListaModel> list = db.Database.SqlQuery<CargoListaModel>(SP, cargo).ToList<CargoListaModel>();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CargoCircuitoModel> TotalCargoCircuito(string SP, Guid ca)
        {
            var cargo = new SqlParameter("@CARGO", ca);

            try
            {
                List<CargoCircuitoModel> lista = db.Database.SqlQuery<CargoCircuitoModel>(SP, cargo).ToList<CargoCircuitoModel>();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Blanqueo()
        {
            berraco = String.Empty;
            puntos = String.Empty;
            cucha = String.Empty;
            total = 0;
            t = String.Empty;
        }
    }
}
