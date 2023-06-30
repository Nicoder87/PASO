using Escrutinio.Database;
using Escrutinio.Filters;
using Escrutinio.Helpers;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class MesasEscrutadasController : Controller
    {
        ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        [AuthorizeRule]
        public ActionResult Index()
        {
            var lista = ListaMesasEscrutadas("SP_MESAS_ESCRUTADAS");    

            return View(lista);
        }

        internal List<MesasEscrutadasIndexModel> ListaMesasEscrutadas(string storedProcedure)
        {
            List<MesasEscrutadasIndexModel> lista = db.Database.SqlQuery<MesasEscrutadasIndexModel>
                (storedProcedure).ToList<MesasEscrutadasIndexModel>();

            return lista;
        }

        [AuthorizeRule]
        [HttpGet]
        public ActionResult Ver(Guid Id)
        {
            USUARIO usuario = AccountHelper.GetCurrentUser();
            ViewBag.Usuario = usuario.USUARIO1;

            ViewBag.Cargos = db.CARGO.OrderBy(x => x.ORDEN).ToList();

            var model = new CargaModel();
            model.ListaPostulacion = VerVotos(Id);

            MESA objMesa = db.MESA.Where(m => m.ORID == Id).FirstOrDefault();
            model.MesaModel = new MesaModel();
            model.Mesa = objMesa.NUMERO;

            ESCRUTINIO_CAB em = db.ESCRUTINIO_CAB.Where(m => m.MESA == objMesa.ORID).FirstOrDefault();
            model.Votos_emitidos = em.VOTOS_EMITIDOS;

            if (objMesa != null)
            {
                model.ORID_MESA = objMesa.ORID;
                model.MesaModel.Escuela = objMesa.ESCUELA1.DESCRIPCION;
                model.MesaModel.Circuito = objMesa.ESCUELA1.CIRCUITO1.DESCRIPCION;
                model.MesaModel.CantVotantes = objMesa.CANT_VOTANTES;

                model.MesaModel.Tipo = "NACIONALES";
                if (objMesa.EXTRANJERO) model.MesaModel.Tipo = "EXTRANJEROS";
            }

            model.otros_votos = GetOtrosVotos(Id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Ver(CargaModel model)
        {
            var m = new SqlParameter("@mesa", model.ORID_MESA);

            var a = db.Database.SqlQuery<int>("SP_MESA_BORRAR @mesa", m).FirstOrDefault();

            return RedirectToAction("Index");
        }

        public List<PostulacionModel> VerVotos(Guid mesa)
        {
            List<PostulacionModel> lista = GetListaEdit("SP_VOTOS_MESA_DETALLE @mesa", mesa);

            return lista;
        }

        public List<TotalesOtrosVotosModel> GetOtrosVotos(Guid mesa)
        {
            var mes = new SqlParameter("@mesa", mesa);

            List<TotalesOtrosVotosModel> lista = db.Database.SqlQuery<TotalesOtrosVotosModel>
                ("SP_TOTALES_VOTOS_MESA @MESA", mes).ToList<TotalesOtrosVotosModel>();

            return lista;
        }

        public List<PostulacionModel> GetListaEdit(string storedProcedure, Guid Mesa)
        {
            var mes = new SqlParameter("@mesa", Mesa);

            List<PostulacionModel> lista = db.Database.SqlQuery<PostulacionModel>
                (storedProcedure, mes).ToList<PostulacionModel>();

            return lista;
        }

    }
}
