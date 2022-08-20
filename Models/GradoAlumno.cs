namespace WSColegio.Models
{
    public class GradoAlumno
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }
        public int GradoId { get; set; }
        public string Seccion { get; set; } = null!;
        public string? AlumnoNombre { get; set; }
        public string? GradoNombre { get; set; }
    }
}
