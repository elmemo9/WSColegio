using WSColegio.Responses;

namespace WSColegio.Services.Interfaces
{
    public interface IListService
    {
        public Response GetAlumnos();
        public Response GetGrados();
        public Response GetProfesores();
    }
}
