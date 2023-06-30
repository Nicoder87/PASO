using Escrutinio.Database;
using Escrutinio.Filters;
using Escrutinio.Helpers;
using Escrutinio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escrutinio.Controllers
{
    public class UsuarioController : Controller
    {
        [AuthorizeRule(true)]
        public virtual ActionResult UpdatePassword()
        {
            var currentUser = AccountHelper.GetCurrentUser();
            USUARIO entity = ReadForEditOrDetail(currentUser.ORID);
            UserUpdatePassword model = new UserUpdatePassword() { ORID = entity.ORID };

            return View(model);
        }

        [HttpPost]
        [AuthorizeRule(true)]
        public ActionResult UpdatePassword(UserUpdatePassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    USUARIO oldEntity = ReadForEditOrDetail(model.ORID);
                    oldEntity.PASSWORD = Encryption.Encrypt(model.Password);

                    using (ESCRUTINIOEntities db = new ESCRUTINIOEntities())
                    {
                        db.Entry(oldEntity).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    TempData["msj"] = "updateOK";

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        protected USUARIO ReadForEditOrDetail(Guid id)
        {
            USUARIO entidad = null;

            using (ESCRUTINIOEntities connection = new ESCRUTINIOEntities())
            {
                entidad = connection.USUARIO.FirstOrDefault(x => x.ORID == id);
            }

            return entidad;
        }
    }
}