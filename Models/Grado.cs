namespace WSColegio.Models
{
    public class Grado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int ProfesorId { get; set; }
        public string? ProfesorNombre { get; set; }
    }
}
