using System.ComponentModel.DataAnnotations.Schema;

namespace CloudTrabajoBimestral.Models
{
    public class Espacio
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Type { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int Capacity { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Location { get; set; }

        //navigation properties
        public List<Sesion>? Sesiones { get; set; }
    }
}