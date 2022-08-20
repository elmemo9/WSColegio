using System.ComponentModel.DataAnnotations;
using WSColegio.Requests.Validations;

namespace WSColegio.Requests
{
    public class ProfesorRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Nombre { get; set; } = null!;
        
        [Required, MaxLength(50)] 
        public string Apellidos { get; set; } = null!;

        [Required, Genero]
        public string Genero { get; set; } = null!;
    }
}
