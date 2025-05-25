using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class SesionPonente
    {
        public int Id { get; set; }

        [Required]
        public int SesionId { get; set; }

        [Required]
        public int PonenteId { get; set; }

        // Navigation properties
        public Sesion? Sesion { get; set; }
        public Ponente? Ponente { get; set; }
    }
}
