using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Asistencia
    {
        public int Id { get; set; }
        public DateTime fechaAsistencia { get; set; }
        public bool estado { get; set; }

        //FK
        public int sesionId { get; set; }
        public int inscripcionId {get; set;}

        //navigation properties
        public Inscripcion? Inscripcion { get; set; }
        public Sesion? Sesion { get; set; }


    }
}
