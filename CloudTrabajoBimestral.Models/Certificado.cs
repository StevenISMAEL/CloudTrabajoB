using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Certificado
    {
        public int Id { get; set; }
        public DateTime fechaEmision { get; set; }
        public string UrlDescarga { get; set; }

        //FK
        public int InscripcionID { get; set; }

        //navigation
        public Inscripcion? Inscripcion { get; set; }
    }
}
