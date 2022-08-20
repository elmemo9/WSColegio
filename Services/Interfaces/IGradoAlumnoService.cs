
using WSColegio.Requests;
using WSColegio.Responses;

namespace WSColegio.Services.Interfaces
{
    public interface IGradoAlumnoService
    {
        public Response Add(GradoAlumnoRequest gradoAlumnoRequest);
        public Response Delete(int id);
        public Response Get();
        public Response Update(GradoAlumnoRequest gradoAlumnoRequest);
    }
}
