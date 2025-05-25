using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Pago
    {

        public int Id { get; set; }
        public double monto { get; set; }
        public DateTime fechaPago { get; set; }
        public string medioPago { get; set; }
        public bool estado { get; set; }

        //FK
        public int InscripcionID { get; set; }
        
        //navigation 
        public Inscripcion? Inscripcion { get; set; }


    }
}
