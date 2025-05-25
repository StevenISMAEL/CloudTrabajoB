using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudTrabajoBimestral.Models
{
    public class Ponente
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Lastname { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Especialidad { get; set; }

        //navigation properties
        public List<Sesion>? Sesiones { get; set; }
    }
}