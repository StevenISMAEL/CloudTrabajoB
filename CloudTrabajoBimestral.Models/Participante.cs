using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Participante
    {
        [Key, Required]
        public string Cedula { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //navigation
        public List<Inscripcion>? Inscripciones { get; set; }
    }
}
