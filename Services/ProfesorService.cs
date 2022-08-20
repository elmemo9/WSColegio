using WSColegio.Models;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IConfiguration _configuration;
        public ProfesorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response Add(ProfesorRequest profesorRequest)
        {
            Response response = new();
            Profesor profesor = new()
            {
                Nombre = profesorRequest.Nombre,
                Apellidos = profesorRequest.Apellidos,
                Genero = profesorRequest.Genero
            };
            int result = new DBContext(_configuration).AddProfesor(profesor);
            if (result > 0)
            {
                profesor.Id = result;
                response.Success = 1;
                response.Message = "El profesor se insertó correctamente";
                response.Data = profesor;
            }
            else
            {
                response.Message = "No se ha podido insertar";
            }
            return response;
        }

        public Response Delete(int id)
        {
            Response response = new();
            if (!new DBContext(_configuration).ProfesorExists(id))
            {
                response.Message = "El profesor no existe";
                return response;
            }
            int result = new DBContext(_configuration).DeleteProfesor(id);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El profesor se eliminó correctamente";
            }
            else
            {
                response.Message = "No se ha podido eliminar";
            }
            return response;
        }

        public Response Get()
        {
            Response response = new();

            List<Profesor>? profesors = new DBContext(_configuration).GetProfesores(null);
            if (profesors == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", profesors.Count);
                response.Data = profesors;
            }
            return response;
        }

        public Response Update(ProfesorRequest profesorRequest)
        {
            Response response = new();
            if (!new DBContext(_configuration).ProfesorExists(profesorRequest.Id))
            {
                response.Message = "El profesor no existe";
                return response;
            }
            Profesor profesor = new()
            {
                Id = profesorRequest.Id,
                Nombre = profesorRequest.Nombre,
                Apellidos = profesorRequest.Apellidos,
                Genero = profesorRequest.Genero,
            };
            int result = new DBContext(_configuration).UpdateProfesor(profesor);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El profesor se actualizó correctamente";
                response.Data = profesor;
            }
            else
            {
                response.Message = "No se ha podido actualizar";
            }
            return response;
        }
    }
}
