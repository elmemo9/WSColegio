using WSColegio.Responses;
using WSColegio.Requests;

namespace WSColegio.Services.Interfaces
{
    public interface IAlumnoService
    {
        
        public Response Add(AlumnoRequest alumnoRequest);
        public Response Delete(int id);
        public Response Get();
        public Response Update(AlumnoRequest alumnoRequest);
    }
}
