using Escrutinio.Database;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class CargaController : Controller
    {
        private ESCRUTINIOEntities db = new ESCRUTINIOEntities();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Cargos = db.CARGO.OrderBy(x => x.ORDEN).ToList();

            var model = new CargaModel
            {
                MesaModel = new MesaModel(),
                ListaPostulacion = new List<PostulacionModel>()
            };

            model.ValidosTotales = new ValidosTotalesModel
            {
                VtOtrosVotos = new List<OtrosVotosModel>(),
                VtMesa = new MESA(),
                VtCargos = new List<CARGO>()
            };

            if (TempData["msj"] != null && !string.IsNullOrEmpty(TempData["msj"].ToString()))
            {
                ViewBag.MessageOk = TempData["msj"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CargaModel model)
        {
            //if (model.Postulacion == null)
            //{
            //    return RedirectToAction("Create");
            //}

            MESA mesa = db.MESA.Where(m => m.NUMERO == model.Mesa).FirstOrDefault();

            if (mesa != null)
            {
                ESCRUTINIO_CAB ec = db.ESCRUTINIO_CAB.Where(m => m.MESA == mesa.ORID).FirstOrDefault();
                if (ec != null)
                {
                    return RedirectToAction("Create");
                }

                ESCRUTINIO_MESA em = db.ESCRUTINIO_MESA.Where(m => m.MESA == mesa.ORID).FirstOrDefault();
                if (em != null)
                {
                    return RedirectToAction("Create");
                }

                ESCRUTINIO_DET ed = db.ESCRUTINIO_DET.Where(m => m.MESA == mesa.ORID).FirstOrDefault();
                if (ed != null)
                {
                    return RedirectToAction("Create");
                }
            }


            try
            {
                if (mesa != null)
                {
                    EscrutinioCabecera(mesa.ORID, model);
                    EscrutinioDetalle(mesa.ORID, model);
                    EscrutinioMesa(mesa.ORID, model);
                }
                TempData["msj"] = "La MESA " + model.Mesa + " se cargó correctamente";
                return RedirectToAction("Create");
            }
            catch (Exception)
            {
                throw;
            }
        }



        private void EscrutinioCabecera(Guid IdMesa, CargaModel model)
        {
            ESCRUTINIO_CAB ec = new ESCRUTINIO_CAB
            {
                ORID = Guid.NewGuid(),
                MESA = IdMesa,
                VOTOS_EMITIDOS = model.Votos_emitidos,
                SOBRES = 350,
                INICIO = (model.InicioCarga).TimeOfDay,
                FIN = (DateTime.Now).TimeOfDay,
                USUARIO = Guid.Parse("28b9b8d7-6185-4c1b-b22f-d098614dd0e0") // smyp
            };

            db.ESCRUTINIO_CAB.Add(ec);
            db.SaveChanges();
        }

        private void EscrutinioMesa(Guid IdMesa, CargaModel model)
        {
            MESA mesa = db.MESA.Where(m => m.ORID == IdMesa).FirstOrDefault();

            if (mesa.EXTRANJERO)
            {
                ViewBag.Cargos = db.CARGO.Where(c => c.APLICA_EXT == true).OrderBy(x => x.ORDEN).ToList();
            }
            else
            {
                ViewBag.Cargos = db.CARGO.OrderBy(x => x.ORDEN).ToList();
            }

            int mc = 0; // MESA-CARGO

            foreach (var c in ViewBag.Cargos)
            {
                ESCRUTINIO_MESA em = new ESCRUTINIO_MESA
                {
                    ORID = Guid.NewGuid(),
                    MESA = IdMesa,
                    CARGO = c.ORID,
                    NULOS = model.Nulos[mc],
                    RECURRIDOS = model.Recurridos[mc],
                    IMPUGNADOS = model.Impugnados[mc],
                    BLANCOS = model.Blancos[mc],
                    VALIDOS = model.Validos[mc]
                };

                mc = mc + 1;

                db.ESCRUTINIO_MESA.Add(em);
                db.SaveChanges();
            }
        }

        private void EscrutinioDetalle(Guid IdMesa, CargaModel model)
        {
            for (int i = 0; i < model.Postulacion.Length; i++)
            {
                ESCRUTINIO_DET ed = new ESCRUTINIO_DET
                {
                    ORID = Guid.NewGuid(),
                    POSTULACION = Guid.Parse(model.Postulacion[i]),
                    MESA = IdMesa,
                    VOTOS = model.Votos[i]
                };

                db.ESCRUTINIO_DET.Add(ed);
                db.SaveChanges();
            }
        }

        public ActionResult GetPostulaciones(int mesa)
        {
            var lista = new List<PostulacionModel>();

            MESA objMesa = db.MESA.Where(m => m.NUMERO == mesa).FirstOrDefault();
            lista = GetLista("SP_VOTOS_INSERT @mesa_ext", objMesa.EXTRANJERO);

            return PartialView("_Postulaciones", lista);
        }

        public ActionResult GetValidosTotales(int mesa)
        {
            ValidosTotalesModel model = new ValidosTotalesModel();

            model.VtMesa = db.MESA.Where(m => m.NUMERO == mesa).FirstOrDefault();

            OtrosVotosModel ov = new OtrosVotosModel();
            model.VtOtrosVotos = ov.GetOtrosVotos();

            model.VtCargos = db.CARGO.OrderBy(x => x.ORDEN).ToList();

            return PartialView("_ValidosTotales", model);
        }



        public ActionResult GetDatosMesa(int mesa)
        {
            MESA objMesa = db.MESA.Where(m => m.NUMERO == mesa).FirstOrDefault();
            MesaModel model = new MesaModel();

            if (objMesa != null)
            {
                model.Escuela = objMesa.ESCUELA1.DESCRIPCION;
                model.Circuito = objMesa.ESCUELA1.CIRCUITO1.DESCRIPCION;
                model.CantVotantes = objMesa.CANT_VOTANTES;

                model.Tipo = "NACIONALES";
                if (objMesa.EXTRANJERO) model.Tipo = "EXTRANJEROS";
            }

            return PartialView("_DatosMesa", model);
        }

        public ActionResult VerificoMesa(int mesa)
        {
            MESA objMesa = db.MESA.Where(m => m.NUMERO == mesa).FirstOrDefault();
            string VM = String.Empty;

            if (objMesa == null)
            {
                VM = "0"; // NO EXISTE
            }
            else
            {
                ESCRUTINIO_CAB ec = db.ESCRUTINIO_CAB.Where(e => e.MESA == objMesa.ORID).FirstOrDefault();
                if (ec == null)
                {
                    DateTime date = DateTime.Now;
                    string sFecha = Convert.ToString(date);
                    VM = sFecha;
                }
                else
                {
                    VM = "1"; // YA ESTA CARGADA
                }
            }

            var data = new { vm = VM };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public List<PostulacionModel> GetLista(string storedProcedure, bool e)
        {
            var ext = new SqlParameter("@mesa_ext", e);
            List<PostulacionModel> lista = db.Database.SqlQuery<PostulacionModel>(storedProcedure, ext).ToList<PostulacionModel>();

            return lista;
        }


    }
}