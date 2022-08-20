using WSColegio.Requests;
using WSColegio.Responses;

namespace WSColegio.Services.Interfaces
{
    public interface IProfesorService
    {
        public Response Add(ProfesorRequest profesorRequest);
        public Response Delete(int id);
        public Response Get();
        public Response Update(ProfesorRequest profesorRequest);
    }
}
