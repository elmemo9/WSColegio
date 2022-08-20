using WSColegio.Requests;
using WSColegio.Responses;

namespace WSColegio.Services.Interfaces
{
    public interface IGradoService
    {
        public Response Add(GradoRequest gradoRequest);
        public Response Delete(int id);
        public Response Get();
        public Response Update(GradoRequest gradoRequest);
    }
}
