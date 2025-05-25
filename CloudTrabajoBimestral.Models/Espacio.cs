namespace CloudTrabajoBimestral.Models
{
    public class Espacio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }


        //navigation properties
        public List<Sesion>? Sesiones { get; set; } 

    }
}
