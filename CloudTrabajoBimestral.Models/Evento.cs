using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudTrabajoBimestral.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string type { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string location { get; set; }

        public int maxCapacity { get; set; }

        //navigation
        public List<Sesion>? Sesion { get; set; }
        public List<Inscripcion>? Inscripciones { get; set; }
    }
}