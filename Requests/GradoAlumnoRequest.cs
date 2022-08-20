using System.ComponentModel.DataAnnotations;
using WSColegio.Requests.Validations;

namespace WSColegio.Requests
{
    public class GradoAlumnoRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int AlumnoId { get; set; }
        
        [Required, Range(0, int.MaxValue)]
        public int GradoId { get; set; }

        [Required, MaxLength(50)]
        public string Seccion { get; set; } = null!;
    }
}
