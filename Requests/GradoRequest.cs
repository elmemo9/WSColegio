using System.ComponentModel.DataAnnotations;
using WSColegio.Requests.Validations;

namespace WSColegio.Requests
{
    public class GradoRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;
        
        [Required, Range(0, int.MaxValue)]
        public int ProfesorId { get; set; }
    }
}
