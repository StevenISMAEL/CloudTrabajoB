using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public DateTime fechaInscripcion { get; set; }
        public bool estado { get; set; }

        //FK
        public int EventoId { get; set; }   
        public required string Cedula { get; set; }

        //navigation properties

        public List<Asistencia>? Asistencias { get; set; }
        public List<Pago>? Pagos { get; set; }
        public Participante? Participante { get; set; }
        public List<Certificado>? Certificados { get; set; }
        public Evento? Evento { get; set; }



    }
}
