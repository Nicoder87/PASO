using System.Collections.Generic;

namespace Escrutinio.Models
{
    public class OtrosVotosModel
    {
        public string Id;
        public string Descripcion;

        public OtrosVotosModel() { }

        public OtrosVotosModel(string Id, string Descripcion)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
        }

        public List<OtrosVotosModel> GetOtrosVotos()
        {
            List<OtrosVotosModel> lista = new List<OtrosVotosModel>
            {
                new OtrosVotosModel("Nulos", "NULOS"),
                new OtrosVotosModel("Recurridos", "RECURRIDOS"),
                new OtrosVotosModel("Impugnados", "IMPUGNADOS"),
                new OtrosVotosModel("Blancos", "BLANCOS")
            };

            return lista;
        }
    }
}