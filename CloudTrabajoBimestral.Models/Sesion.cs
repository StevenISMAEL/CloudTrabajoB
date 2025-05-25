using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTrabajoBimestral.Models
{
    public class Sesion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFin { get; set;}

        //FK
        public int EspacioID {  get; set; }
        public int EventoID { get; set; }

        //navigation properties
        public Espacio? Espacio { get; set; }
        public Evento? Evento { get; set; }
        public List<Asistencia>? Asistencias { get; set; }
        public List<Ponente>? Ponentes { get; set;} //


    }
}
